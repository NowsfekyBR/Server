using Godot;
using System;
using System.Collections.Generic;

public class NetWorking : Node
{
    UDPServer server = new UDPServer();
    
    List<Godot.PacketPeerUDP> clients = new List<Godot.PacketPeerUDP>();

    public override void _Ready()
    {
        server.Listen(8080, "127.0.0.1");
        GD.Print("Server started...");
    }

    public override void _Process(float delta)
    {
        server.Poll();

        if (server.IsConnectionAvailable())
        {
            PacketPeerUDP client = server.TakeConnection();
            var packet = client.GetPacket();
            GD.Print("Accepted client: " + client.GetPacketIp() + " : " + client.GetPacketPort());
            GD.Print("Accepted packet: " + packet.GetStringFromUTF8());

            client.PutPacket(packet);
            
            clients.Add(client);

            for(int i = 0; i < clients.Count; i++)
            {
                GD.Print(i);
            }

        }
    }
}
