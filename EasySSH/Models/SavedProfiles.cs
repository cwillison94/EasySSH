﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasySSH
{
    public class SavedProfile
    {
        public int ProfileID { get; set; }
        public string ProfileName { get; set; }
        public string UserName { get; set; }
        public string Host { get; set; }
        public string Password { get; set; }
        public int Port { get; set; }
        public string QuickName { get { return this.ProfileName + " : " + this.UserName + "@" + this.Host; } }
        public string PasswordMasked
        {
            get
            {
                StringBuilder builder = new StringBuilder();

                for (int i = 0; i < this.Password.Length; i++)
                {
                    builder.Append("*");
                }

                return builder.ToString();
            }
        }
    }

    public class SavedProfiles
    {
        private List<SavedProfile> profiles = new List<SavedProfile>();
        private static SavedProfiles instance = null;

        private SavedProfiles()
        {
            #region Dummy Data
            // fill in with some dummy profiles
            this.profiles.Add(new SavedProfile()
            {
                ProfileID = 1,
                ProfileName = "Moore",
                UserName = "cwillison",
                Host = "moore.cas.mcmaster.ca",
                Password = "psswd123",
                Port = 22
            });

            // fill in with some dummy profiles
            this.profiles.Add(new SavedProfile()
            {
                ProfileID = 2,
                ProfileName = "Mills",
                UserName = "cwillison",
                Host = "mills.cas.mcmaster.ca",
                Password = "psswd123",
                Port = 22
            });

            // fill in with some dummy profiles
            this.profiles.Add(new SavedProfile()
            {
                ProfileID = 3,
                ProfileName = "Raspberry Pi",
                UserName = "pi",
                Host = "192.168.127.1",
                Password = "raspberry",
                Port = 22
            });

            // fill in with some dummy profiles
            this.profiles.Add(new SavedProfile()
            {
                ProfileID = 4,
                ProfileName = "Router",
                UserName = "C3PO",
                Host = "192.168.0.1",
                Password = "R2D2@starwars",
                Port = 22
            });

            #endregion
        }

        public static SavedProfiles Instance 
        {
            get 
            {
                if (instance == null)
                {
                    instance = new SavedProfiles();
                }

                return instance;
            }
        }

        public SavedProfile[] ListAll()
        {
            return this.profiles.ToArray();
        }

        public SavedProfile Add(string description, string userName, string host, string password, int port) 
        {
            int maxID = 0;
            var maxProfile = this.profiles.OrderByDescending(x => x.ProfileID).FirstOrDefault();
            if (maxProfile != null) maxID = maxProfile.ProfileID + 1;

            var newProfile = new SavedProfile() 
            { 
                ProfileID = maxID, 
                ProfileName = description,
                UserName = userName,
                Host = host,
                Password = password,
                Port = port
            };

            this.profiles.Add(newProfile);

            return newProfile;
        }

        public void Remove(int profileID)
        {
            this.profiles.RemoveAll(x => x.ProfileID == profileID);
        }

    }
}
