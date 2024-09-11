using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using quanLyNo_BE.Common;
using quanLyNo_BE.Models;
using System.Collections.Generic;
using System;
using quanLyNo_BE.Repository;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace quanLyNo_BE.Services
{
    public class RelativeInformationService : Repository<RelativeInformation>
    {
        private readonly ApplicationDbContext dc;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public RelativeInformationService(ApplicationDbContext dc, IHttpContextAccessor httpContextAccessor)
            : base(dc, httpContextAccessor)
        {
            this.dc = dc;
            _httpContextAccessor = httpContextAccessor;
        }

        public IActionResult CreateRelativeInformationService(RelativeInformation relativeInformation)
        {
            var userId = GetUserIdFromToken();
            if (userId == null)
            {
                return new JsonResult(new { message = Constants.Message.UserIdEmpty });
            }
            if (relativeInformation == null)
            {
                return new JsonResult(new { message = Constants.Message.NoDataFound });
            }
            typeof(RelativeInformation).GetProperty("UserId")?.SetValue(relativeInformation, userId);
            Create(relativeInformation);
            return new JsonResult(new { message = Constants.Message.CreatedSuccessfully });
        }
        public IEnumerable<RelativeInformation> GetRelativeInformation()
        {
            var userId = GetUserIdFromToken();
            if (string.IsNullOrEmpty(userId))
            {
                new JsonResult(new { message = Constants.Message.UserIdEmpty });
            }
            var relativeInformationData = base.Index();
            var listData = relativeInformationData
            .Where(x => IsUserIdMatch(x, userId)).ToList();
            return listData;
        }
        public IActionResult GetRelativeInformationById(int id)
        {
            var userId = GetUserIdFromToken();
            if (string.IsNullOrEmpty(userId))
            {
                new JsonResult(new { message = Constants.Message.UserIdEmpty });
            }
            var relativeInformationData = base.GetById(id);
            if (relativeInformationData == null)
            {
                return new JsonResult(new { message = Constants.Message.NoDataFound });
            }
            return new JsonResult(relativeInformationData);
        }
        public IActionResult UpdateRelativeInformation(int id, RelativeInformation relativeInformation)
        {
            var userId = GetUserIdFromToken();
            if (string.IsNullOrEmpty(userId))
            {
                return new JsonResult(new { message = Constants.Message.UserIdEmpty });
            }
            typeof(RelativeInformation).GetProperty("id")?.SetValue(relativeInformation, id);
            Update(relativeInformation);
            return new JsonResult(new { message = Constants.Message.UpdatedSuccessfully });
        }
        // public IActionResult DeleteRelativeInformation(int id)
        // {

        // }
    }

}