﻿namespace LocalFriendzApi.Application.Request
{
    public class UpdateContactRequest
    {
        public string? Name { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public string? AreaCode { get; set; }
    }
}
