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
	static int before_state = -1;



	if(val < under_weak)
	{		
		if(before_state != 1)
		{
			send(ServerSock, NONE, 4, 0);
			before_state = 1;
		}		
	}else if( val < weak_strong)
	{
		if(before_state != 2)
		{
			send(ServerSock, WEAK, 4, 0);
			before_state = 2;
		}
		
	}else if(val < strong_hard)
	{
		if(before_state != 3)
		{
			send(ServerSock, STRING, 4, 0);
			before_state = 3;
		}

	}else
	{
		if(before_state !=4)
		{
			send(ServerSock, HARD, 4, 0);
			before_state = 4;
		}		
	}

	//sprintf(buf,"%04d", val);

	//MessageBox(hWnd, buf, "CHECK", MB_OK);
	//len = strlen(buf);
	//send(ServerSock, buf, len, 0);
}