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

using NtApiDotNet;
using NtApiDotNet.Ndr.Marshal;
using NtApiDotNet.Win32.Rpc;
using NtApiDotNet.Win32.Rpc.Transport;
using OleViewDotNet.Database;
using OleViewDotNet.Interop;
using OleViewDotNet.Marshaling;
using OleViewDotNet.TypeManager;
using OleViewDotNet.Utilities;
using System;
using System.Collections.Generic;

namespace OleViewDotNet.Rpc.Transport;

public sealed class RpcChannelBufferClientTransport : IRpcClientTransport, INdrTransportMarshaler
{
    private readonly object m_object;
    private readonly COMRegistry m_registry;
    private RpcChannelBuffer m_buffer;

    internal object GetObject() => m_object;

    public RpcChannelBufferClientTransport(object obj, Guid interface_id, COMRegistry registry)
    {
        m_object = obj ?? throw new ArgumentNullException(nameof(obj));
        m_buffer = RpcChannelBuffer.FromObject(m_object, interface_id);
        m_registry = registry;
    }

    public bool Connected => m_buffer?.IsConnected ?? false;

    public string Endpoint => "DCOM";

    public string ProtocolSequence => "DCOM";

    public bool Authenticated => true;

    public RpcAuthenticationType AuthenticationType => RpcAuthenticationType.Default;

    public RpcAuthenticationLevel AuthenticationLevel => RpcAuthenticationLevel.Default;

    public RpcServerProcessInformation ServerProcess => null;

    public int CallId => 0;

    public bool SupportsMultipleSecurityContexts => false;

    public IReadOnlyList<RpcTransportSecurityContext> SecurityContext => new RpcTransportSecurityContext[0];

    public RpcTransportSecurityContext CurrentSecurityContext { get => null; set => throw new NotImplementedException(); }

    public bool SupportsSynchronousPipes => false;

    public RpcTransportSecurityContext AddSecurityContext(RpcTransportSecurity transport_security)
    {
        throw new NotImplementedException();
    }

    public void Bind(Guid interface_id, Version interface_version, Guid transfer_syntax_id, Version transfer_syntax_version)
    {
        throw new NotSupportedException("Binding not supporting on this transport.");
    }

    public void Disconnect()
    {
        Dispose();
    }

    public void Dispose()
    {
        m_buffer?.Dispose();
        m_buffer = null;
    }

    public RpcClientResponse SendReceive(int proc_num, Guid objuuid, NdrDataRepresentation data_representation, byte[] ndr_buffer, IReadOnlyCollection<NdrSystemHandle> handles)
    {
        if (!Connected)
            throw new InvalidOperationException("Transport must be connected before sending.");
        if (handles.Count > 0)
            throw new ArgumentException("Transport doesn't support sending kernel handles.", nameof(handles));
        return new(m_buffer.SendReceive(ndr_buffer, proc_num), new NtObject[0], this);
    }

    NdrInterfacePointer INdrTransportMarshaler.MarshalComObject(INdrComObject obj, Guid iid)
    {
        if (obj is not ICOMObjectWrapper wrapper)
        {
            throw new NotSupportedException("Only support wrapped objects on this transport.");
        }

        return new NdrInterfacePointer(COMUtilities.MarshalObject(wrapper.Unwrap(), iid, m_buffer.DestContext, MSHLFLAGS.NORMAL));
    }

    INdrComObject INdrTransportMarshaler.UnmarshalComObject(NdrInterfacePointer intf)
    {
        COMObjRef objref = COMObjRef.FromArray(intf.Data);
        return COMTypeManager.Wrap(COMUtilities.UnmarshalObject(objref), objref.Iid, m_registry);
    }

    INdrComObject INdrTransportMarshaler.QueryComObject(INdrComObject obj, Guid iid)
    {
        if (obj is not RpcClientBase client || !client.Connected)
        {
            throw new ArgumentException("COM object must be a connected RPC client.");
        }

        if (client.Transport is not RpcChannelBufferClientTransport transport)
        {
            throw new ArgumentException("Transport must be a channel buffer client.");
        }

        return COMTypeManager.Wrap(transport.m_object, iid, m_registry);
    }
}
