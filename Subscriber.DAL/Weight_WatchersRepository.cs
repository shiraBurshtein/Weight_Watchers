using Microsoft.EntityFrameworkCore;
using Subscriber.CORE.Interface_DAL;
using Subscriber.CORE.Response;
using Subscriber.Data;
using Subscriber.Data.entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Subscriber.DAL
{
    public class Weight_WatchersRepository : IWeight_WatchersRepository
    {
        readonly Weight_WatchersContext _weightWatchersContext;
        public Weight_WatchersRepository(Weight_WatchersContext weightWatchersContext)
        {
            _weightWatchersContext = weightWatchersContext;
        }
        public Task<BaseResponseGeneric<CardResponse>> GetCardDetails(int id)
        {
            try
            {
                var response = new BaseResponseGeneric<CardResponse>();
                SubscribeCard Card = _weightWatchersContext.Cards.Where(p => p.Id == id).FirstOrDefault();
                if (Card != null)
                {
                    Subscribers subscriber = _weightWatchersContext.Subscribers.Where(p => p.Id == Card.Subscriber_Id).FirstOrDefault();

                    if (subscriber != null)
                    {
                        response.Succeed = true;
                        response.Status = "succed";
                        response.Response = new CardResponse();
                        response.Response.Id = id;
                        response.Response.Weight = Card.Weight;
                        response.Response.Height = Card.Height;
                        response.Response.BMI = Card.BMI;
                        response.Response.FirstName = subscriber.Firstname;
                        response.Response.LastName = subscriber.Lastname;
                    }
                    else
                    {
                        response.Succeed = false;
                        response.Status = "failed to find subscriber";
                    }

                }
                else
                {
                    response.Succeed = false;
                    response.Status = "failed to find card";
                }
                //     return response;
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception("Get card Failed");
            }
        }
        public Task<BaseResponseGeneric<int>> Login(string email,string password)
        {
            try
            {
                var response = new BaseResponseGeneric<int>();
                Subscribers subscriber = _weightWatchersContext.Subscribers.Where(p => p.Email == email && p.Password == password).FirstOrDefault();
                if (subscriber != null)
                {
                    SubscribeCard card = _weightWatchersContext.Cards.Where(c => c.Subscriber_Id == subscriber.Id).FirstOrDefault();
                    if (card != null)
                    {
                        response.Succeed = true;
                        response.Status = "succeed";
                        response.Response = card.Id;
                    }
                    else
                    {
                        response.Response = 0;
                        response.Succeed = false;
                        response.Status = "failed to find card";
                    }
                }
                else
                {
                    response.Response = 0;
                    response.Succeed = false;
                    response.Status = "failed to find subscriber";
                }

              //  return response;
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(" 401 failed to login");
            }

        }

        public async Task<BaseResponseGeneric<bool>> Register(Subscribers subscriber, double height)
        {
            try
            {
                var response = new BaseResponseGeneric<bool>();
                var createdSubscriber = await _weightWatchersContext.Subscribers.AddAsync(subscriber);
                await _weightWatchersContext.SaveChangesAsync();
                SubscribeCard defaultCard = new SubscribeCard
                {
                    OpenDate = DateTime.Now,
                    UpdateDate = DateTime.Now,
                    BMI = 0,
                    Height = height
                };
                _weightWatchersContext.Cards.AddAsync(defaultCard);
                await _weightWatchersContext.SaveChangesAsync();
                response.Succeed = true;
                response.Response = true;
                response.Status = "succeed";
            }
            catch (Exception ex)
            {
                throw new Exception("Register Failed");
            }  
         //   return response;
            return null;
        }
        public async Task<bool> IsEmailExists(string email)
        {
            return await _weightWatchersContext.Subscribers.AnyAsync(s => s.Email == email);
        }
    }
}
