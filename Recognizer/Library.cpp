#include<stdio.h>
#include<stdlib.h>
#include<string.h>
#include<windows.h>
#include"recognizer.h"



extern HWND hWnd;
extern int under_weak;
extern int weak_strong;
extern int strong_hard;
extern int over_hard;


bool init_complete = false;
SOCKET ServerSock;


#define NONE	"NONE"
#define WEAK	"WEAK"
#define STRING	"STNG"
#define HARD	"HARD"


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

	if(val < under_weak)
	{
		send(ServerSock, NONE, 4, 0);
	}else if( val < weak_strong)
	{
		send(ServerSock, WEAK, 4, 0);
	}else if(val < strong_hard)
	{
		send(ServerSock, STRING, 4, 0);
	}else
	{
		send(ServerSock, HARD, 4, 0);
	}

	//sprintf(buf,"%04d", val);

	//MessageBox(hWnd, buf, "CHECK", MB_OK);
	//len = strlen(buf);
	//send(ServerSock, buf, len, 0);
}