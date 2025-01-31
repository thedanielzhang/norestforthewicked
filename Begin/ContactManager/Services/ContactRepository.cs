﻿using ContactManager.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ContactManager.Services
{
    public class ContactRepository
    {
        private const string CacheKey = "ContactStore";

        public ContactRepository()
        {
            var ctx = HttpContext.Current;

            if (ctx != null)
            {
                if (ctx.Cache[CacheKey] == null)
                {
                    var contacts = new Contact[]
                    {
                        new Contact
                        {
                            Id = 1, Name = "Lebron James"
                        },
                        new Contact
                        {
                            Id = 2, Name = "Kyrie Irving"
                        }
                    };

                    ctx.Cache[CacheKey] = contacts;
                }
            }
        }

        public Contact[] GetAllContacts()
        {
            var ctx = HttpContext.Current;

            if (ctx != null)
            {
                return (Contact[])ctx.Cache[CacheKey];
            }
            else
            {
                return new Contact[]
                {
                    new Contact
                    {
                        Id = 0,
                        Name = "Placeholder"
                    }
                };
            }
        }

        public bool SaveContact(Contact contact)
        {
            var ctx = HttpContext.Current;

            if (ctx != null)
            {
                try
                {
                    var currentData = ((Contact[])ctx.Cache[CacheKey]).ToList();
                    currentData.Add(contact);
                    ctx.Cache[CacheKey] = currentData.ToArray();
                    return true;
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
    }
}