﻿using UberDinner.Domain.Entities;

namespace UberDinner.Application.Common.Interfaces.Persistence
{
    public interface IUserRepository
    {
        User? GetUserByEmail(string email);
        void Add(User user);
    }
}
