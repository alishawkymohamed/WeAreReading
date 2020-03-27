using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Models.DbModels;
using Services.Contracts;

namespace WebApi
{
    public class BaseController<TDbEntity> : Controller where TDbEntity : BaseEntity
    {
        private readonly IService<TDbEntity> service;

        public BaseController(IService<TDbEntity> service)
        {
            this.service = service;
        }
    }
}
