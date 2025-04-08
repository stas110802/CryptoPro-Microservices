using AutoMapper;
using CryptoPro.ClientsService.Domain.Dtos;
using CryptoPro.ClientsService.Domain.Entities;

namespace CryptoPro.ClientsService.Application.Profilies;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<UserEntity, UserReadDto>();
        CreateMap<UserReadDto, UserEntity>();
        CreateMap<UserCreateDto, UserEntity>();
        CreateMap<UserUpdateDto, UserEntity>();
    }
}