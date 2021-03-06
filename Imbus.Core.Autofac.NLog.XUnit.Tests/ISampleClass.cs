﻿using Imbus.Core.Autofac.NLog.Interfaces;

// ReSharper disable UnusedMember.Global

namespace Imbus.Core.Autofac.NLog.XUnit.Tests
{
    public interface ISampleClass
    {
        IImbusLogger GetLogger();
        void SampleMessage(string message);
    }
}