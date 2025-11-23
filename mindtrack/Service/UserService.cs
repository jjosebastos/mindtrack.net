using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using mindtrack.Connection;
using mindtrack.DTO.Request;
using mindtrack.DTO.Response;
using mindtrack.Model;
using mindtrack.Repository;

namespace mindtrack.Service
{
    public class UserService 
    {

        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository) {
            _userRepository = userRepository; 
        }
        
        public async Task<UserResponse> SaveUserAsync(UserDto dto)
        {
            var existingUser = await _userRepository.GetByUsernameAsync(dto.Email);
            if (existingUser != null)
            {
                throw new Exception("E-mail já cadastrado!");
            }

            var userMapped = UserMapper(dto);
            var userCreated = await _userRepository.AddAsync(userMapped);
            return ToUserResponse(userCreated);
        }

        public async Task DeleteUserAsync(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if ( user == null)
            {
                throw new KeyNotFoundException("Usuário não encontrado");
            }

            await _userRepository.DeleteAsync(user);
        }

        public async Task<UserResponse> UpdateUserAsync(int id, UserDto dto)
        {
            var userExisting = await _userRepository.GetByIdAsync(id);

            if (userExisting == null)
            {
                throw new KeyNotFoundException("Usuário não encontrado");
            }
            var userMapped = UserUpdateMapper(userExisting, dto);
            var userUpdated = await _userRepository.UpdateAsync(userMapped);

            return ToUserResponse(userUpdated);
        }

        public async Task<UserResponse> GetUserByIdAsync(int id)
        {
            var userEntity = await _userRepository.GetByIdAsync(id);
            if (userEntity == null) return null;
            return ToUserResponse(userEntity);
        }

        public async Task<IEnumerable<UserResponse>> GetAllUsersAsync(int page, int size)
        {
            var users = await _userRepository.GetAllAsync(page, size);
            return users?.Select(ToUserResponse) ?? Enumerable.Empty<UserResponse>();
        }

        private User UserMapper(UserDto dto)
        {
            return new User
            {
                Nome = dto.Nome,
                Email = dto.Email,
                Senha = BCrypt.Net.BCrypt.HashPassword(dto.Senha),
                Genero = dto.Genero,
                Setor = dto.Setor,
                Cargo = dto.Cargo,
                DataAdmissao = dto.DataAdmissao
            };
        }

        private UserResponse ToUserResponse(User user)
        {
            return new UserResponse
            {
                IdUser = user.IdUser,
                Nome = user.Nome,
                Email = user.Email,
                Genero = user.Genero,
                Setor = user.Setor,
                Cargo = user.Cargo,
                DataAdmissao = user.DataAdmissao

            };
        }

        private User UserUpdateMapper(User user, UserDto dto)
        {
            user.Nome = dto.Nome;
            user.Genero = dto.Genero;
            user.Email = dto.Email;
            user.Setor = dto.Setor;
            user.Cargo = dto.Cargo;
            user.DataAdmissao = dto.DataAdmissao;

            if (!string.IsNullOrWhiteSpace(dto.Senha))
            {
                user.Senha = BCrypt.Net.BCrypt.HashPassword(dto.Senha);
            }

            return user;
        }
    }
}
