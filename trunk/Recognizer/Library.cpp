#include<stdio.h>
#include<stdlib.h>
#include<string.h>
#include<windows.h>
#include"recognizer.h"


extern HWND hWnd;
bool init_complete = false;
SOCKET ServerSock;



void socket_init(void)
{
	WSADATA wsaData;
	
	SOCKADDR_IN servAddr;

	
	if(WSAStartup(MAKEWORD(2,2), &wsaData) == SOCKET_ERROR)
	{
		WSACleanup();
		return;
	}
	
	ServerSock = socket( AF_INET, SOCK_DGRAM, 0 ); // 

	if( ServerSock == INVALID_SOCKET )
	{		
		WSACleanup();
		return;
	}
	
	servAddr.sin_family=AF_INET;
	servAddr.sin_addr.s_addr = inet_addr("127.0.0.1");
	servAddr.sin_port=htons(9985);


	if(connect(ServerSock, (struct sockaddr*)&servAddr, sizeof(servAddr)) == SOCKET_ERROR)
	{
		MessageBox(hWnd, "Connet func error", "Error", MB_ICONERROR);
		WSACleanup();
	}

	init_complete = true;
}

void sneding_value(unsigned int val)
{
	char buf[16];
	int len;

	sprintf(buf,"%04d", val);

	//MessageBox(hWnd, buf, "CHECK", MB_OK);
	len = strlen(buf);
	send(ServerSock, buf, len, 0);
}