using AutoMapper;
using Subscriber.CORE.DTO;
using Subscriber.CORE.Interface_DAL;
using Subscriber.CORE.Interface_Service;
using Subscriber.CORE.Response;
using Subscriber.DAL;
using Subscriber.Data.entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Subscriber.Services
{
    public class Weight_WatchersService:IWeight_WatchersService
    {
        readonly IWeight_WatchersRepository _weight_Watchersrepository;
        readonly IMapper _mapper;
        public Weight_WatchersService(IWeight_WatchersRepository weight_Watchersrepository, IMapper mapper)
        {
            _weight_Watchersrepository = weight_Watchersrepository;
            _mapper = mapper;
        }

        public async Task<BaseResponseGeneric<CardResponse>> GetCardDetails(int id)
        {
             var cardDetails = await _weight_Watchersrepository.GetCardDetails(id);
            if (cardDetails != null)
            {
                cardDetails.Status = "card not found";
                cardDetails.Succeed = false;
            }
            else
            {
                cardDetails.Status = "succeed";
                cardDetails.Succeed = true;
            }
            return cardDetails;
        }

        public async Task<BaseResponseGeneric<int>> Login(string email,string password)
        {
            if(!IsValidEmailAddress(email))
            {
                return new BaseResponseGeneric<int> { Response = 0, Status = "EmailIsNotValid", Succeed = false };
            }
            return await _weight_Watchersrepository.Login(password, email);         
        }

        public async Task<BaseResponseGeneric<bool>> Register(SubscriberDTO subscriberDTO)
        {
            if (!await _weight_Watchersrepository.IsEmailExists(subscriberDTO.Email))
            {
                return new BaseResponseGeneric<bool> { Response = false, Status = "emailIsExsit", Succeed = false };
            }

         return await _weight_Watchersrepository.Register(_mapper.Map<Subscribers>(subscriberDTO), subscriberDTO.Height);
      
        }
        public static bool IsValidEmailAddress(string emailAddress)
        {
            if (string.IsNullOrEmpty(emailAddress))
                return false;
            try
            {
                var mailAddress = new MailAddress(emailAddress);
                return mailAddress.Address == emailAddress;
            }
            catch (FormatException)
            {
                return false;
            }
        }


    }
}
