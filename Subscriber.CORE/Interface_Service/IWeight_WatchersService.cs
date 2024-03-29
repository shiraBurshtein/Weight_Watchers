﻿using Subscriber.CORE.Response;
using Subscriber.CORE.DTO;
using Subscriber.Data.entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Subscriber.CORE.Interface_Service
{
    public interface IWeight_WatchersService
    {
        public Task<BaseResponseGeneric<bool>> Register(SubscriberDTO subscriberDTO);
        public Task<BaseResponseGeneric<int>> Login(string password, string email);
        public Task<BaseResponseGeneric<CardResponse>> GetCardDetails(int id);

    }
}
