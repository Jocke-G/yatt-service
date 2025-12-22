using Yatt_Service.Contracts;
using YattService.Common.Entities;

namespace Yatt_Service.Mapping
{
    public static class UserMapping
    {
        public static UserContract ToContract(this UserEntity entity)
        {
            return new UserContract
            {
                Id = entity.Id,
                CreatedAt = entity.CreatedAt,
                LastLogin = entity.LastLogin,
            };
        }
    }
}
