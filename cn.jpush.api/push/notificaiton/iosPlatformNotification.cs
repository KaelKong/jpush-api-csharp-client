﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cn.jpush.api.push.notificaiton
{
    class iosPlatformNotification:PlatformNotification
    {
        public  const String NOTIFICATION_IOS = "ios";
        
        private   const String DEFAULT_SOUND = "";
        private   const String DEFAULT_BADGE = "+1";
    
        private   const String BADGE = "badge";
        private   const String SOUND = "sound";
        private   const String CONTENT_AVAILABLE = "content-available";
        private   const String CATEGORY = "category";
    
        private   const String ALERT_VALID_BADGE = "Badge number should be 0~99999, "
                + "and can be prefixed with + to add, - to minus";


        public bool soundDisabled { get; set; }
        public bool badgeDisabled { get; set; }
        public String sound { get; set; }
        public String badge { get; set; }
        public bool contentAvailable { get; set; }
        public String category { get; set; }

        private iosPlatformNotification(String alert, String sound, String badge,
           bool contentAvailable, bool soundDisabled, bool badgeDisabled,
           String category,
           Dictionary<String, object> extras)
            : base(alert,extras)
        {
            this.sound = sound;
            this.badge = badge;
            this.category = category;
            this.contentAvailable = contentAvailable;
            this.soundDisabled = soundDisabled;
            this.badgeDisabled = badgeDisabled;
        }

        public iosPlatformNotification alert(string alert)
        {
            Debug.Assert(!string.IsNullOrEmpty(alert));
            return new iosPlatformNotification(alert, null, null, false, false, false, null, null);
        }
        override  public String getPlatformName()
        {
            return NOTIFICATION_IOS;
        }
        override  public object toJsonObject()
        {
            Dictionary<string,object> dict = base.toJsonObject() as Dictionary<string, object>;
            if (dict == null)
            {
                dict = new Dictionary<string, object>();
            }

            if (!badgeDisabled)
            {
                if (null != badge)
                {
                    dict.Add(BADGE,this.badge);
                }
                else
                {
                    dict.Add(BADGE, DEFAULT_BADGE);
                }
            }
            if (!soundDisabled)
            {
                if (null != sound)
                {
                    dict.Add(SOUND, sound);
                }
                else
                {
                    dict.Add(SOUND, DEFAULT_SOUND);
                }
            }
            if (contentAvailable)
            {
                dict.Add(CONTENT_AVAILABLE, 1);
            }
            if (null != category)
            {
                dict.Add(CATEGORY,category);
            }
            return dict;
        }
    }
}
