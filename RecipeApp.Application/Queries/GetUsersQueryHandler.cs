using MediatR;
using Microsoft.EntityFrameworkCore;
using RecipeApp.Application.Common;
using RecipeApp.Application.DTOs;

namespace RecipeApp.Application.Queries
{
    public class GetUsersQueryHandler : IRequestHandler<GetUsersQuery, List<UserDto>>
    {
        private readonly IApplicationDbContext _context;

        public GetUsersQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<UserDto>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
        {
            return await _context.Users
                .Where(u => u.IsActive)
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .Select(u => new UserDto
                {
                    Id = u.Id,
                    Username = u.Username,
                    DisplayName = u.DisplayName,
                    ProfilePictureUrl = u.ProfilePictureUrl,
                    CreatedAt = u.CreatedAt
                })
                .ToListAsync(cancellationToken);
        }
    }
}