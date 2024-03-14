﻿//    This file is part of OleViewDotNet.
//    Copyright (C) James Forshaw 2024
//
//    OleViewDotNet is free software: you can redistribute it and/or modify
//    it under the terms of the GNU General Public License as published by
//    the Free Software Foundation, either version 3 of the License, or
//    (at your option) any later version.
//
//    OleViewDotNet is distributed in the hope that it will be useful,
//    but WITHOUT ANY WARRANTY; without even the implied warranty of
//    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//    GNU General Public License for more details.
//
//    You should have received a copy of the GNU General Public License
//    along with OleViewDotNet.  If not, see <http://www.gnu.org/licenses/>.

using NtApiDotNet.Ndr.Marshal;
using System;

namespace OleViewDotNet.Rpc.Clients;

public struct LocalThis : INdrStructure
{
    void INdrStructure.Marshal(NdrMarshalBuffer m)
    {
        m.WriteInt32(dwFlags);
        m.WriteInt32(dwClientThread);
        m.WriteGuid(passthroughTraceActivity);
        m.WriteGuid(callId);
        m.WriteStruct(asyncRequestBlock);
        m.WriteEmbeddedPointer(pTouchedAstaArray, new Action<int>(m.WriteInt32));
        m.WriteEmbeddedPointer(containerPassthroughData, new Action<int>(m.WriteInt32));
    }
    void INdrStructure.Unmarshal(NdrUnmarshalBuffer u)
    {
        throw new NotImplementedException();
    }
    int INdrStructure.GetAlignment()
    {
        return 8;
    }
    public int dwFlags;
    public int dwClientThread;
    public Guid passthroughTraceActivity;
    public Guid callId;
    public LocalThisAsyncRequestBlock asyncRequestBlock;
    public NdrEmbeddedPointer<int> pTouchedAstaArray;
    public NdrEmbeddedPointer<int> containerPassthroughData;
    public static LocalThis CreateDefault()
    {
        return new LocalThis();
    }
    public LocalThis(int dwFlags, int dwClientThread, Guid passthroughTraceActivity, Guid callId, LocalThisAsyncRequestBlock asyncRequestBlock, int? pTouchedAstaArray, int? containerPassthroughData)
    {
        this.dwFlags = dwFlags;
        this.dwClientThread = dwClientThread;
        this.passthroughTraceActivity = passthroughTraceActivity;
        this.callId = callId;
        this.asyncRequestBlock = asyncRequestBlock;
        this.pTouchedAstaArray = pTouchedAstaArray;
        this.containerPassthroughData = containerPassthroughData;
    }
}

