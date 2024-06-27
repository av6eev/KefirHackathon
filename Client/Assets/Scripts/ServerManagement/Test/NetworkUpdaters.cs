﻿using System;
using UnityEngine;
using Updater;

namespace ServerManagement.Test
{
    public class NetworkUpdaters : MonoBehaviour
    {
        public UpdatersList UpdatersList = new();

        private void Update()
        {
            UpdatersList.Update(Time.fixedDeltaTime);
        }
    }
}