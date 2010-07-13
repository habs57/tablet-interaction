#include <Windows.h>
#include <stdio.h>
#include <commctrl.h>
#include "recognizer.h"
#include "resource.h"

extern HWND hWnd;
extern int Sending_flag;
HANDLE	hFile;
HANDLE g_handle;



void sneding_value(unsigned int val);


DWORD WINAPI RecvData( VOID * dummy );

struct coordinate 
{
	short X;

	short reserved;
};

BOOL Open()
{
	HWND hCombo2;
	char str[6];
	int i;
	DWORD			nThreadId;

	hCombo2 = GetDlgItem(hWnd, IDC_COM_PORT);
	i = SendMessage(hCombo2, CB_GETCURSEL, 0, 0);
	SendMessage(hCombo2, CB_GETLBTEXT, i, (LPARAM)str);	


	// 컴포트 열기
	hFile = CreateFile(str,
				GENERIC_READ | GENERIC_WRITE,
				0,				// 비공유
				0,				// 시큐리티 속성:사용안함
				OPEN_EXISTING,	// 기존 파일 오픈
				0, 0 );			// 속성, 템플레이트
	
	if(hFile == INVALID_HANDLE_VALUE)
		MessageBox(0, "Can't Open COM port!","Caution", MB_ICONERROR);
	
	DCB dcb;
	
	// 지정한 통신 디바이스의 현재 DCB 설정 얻기
	// DCB : Device Control Block 디바이스 제어 블럭
	GetCommState(hFile , &dcb);			
											
	// 설정을 변경할 때는 코드 추가
	//		:
	
	// DCB의 지정에 따라 통신 디바이스 구성
	// 하드웨어와 제어 설정 초기화
	SetCommState(hFile , &dcb);			
	
//	wsprintf(szBuffer, "BaudRate %d : ByteSize %d", dcb.BaudRate, dcb.ByteSize);
	
	// 다이알로그박스 캡션에 BaudRate:ByteSize 사이즈 표시
//	SetWindowText(hWnd, szBuffer);

	COMMTIMEOUTS	cTimeout;

	// 현재 설정중인 타임아웃 자료 얻기
	//GetCommTimeouts(hFile, &cTimeout);	

	cTimeout.ReadIntervalTimeout         = 1000;	
	cTimeout.ReadTotalTimeoutMultiplier  = 0;
	cTimeout.ReadTotalTimeoutConstant    = 1000;
	cTimeout.WriteTotalTimeoutMultiplier = 0;
	cTimeout.WriteTotalTimeoutConstant   = 0;

	SetCommTimeouts(hFile, &cTimeout);

//	CreateThread(NULL, 0, (LPTHREAD_START_ROUTINE)RecvData, NULL, 0, &nThreadId);

	CloseHandle(g_handle = CreateThread(NULL, 0, (LPTHREAD_START_ROUTINE)RecvData, NULL, 0, &nThreadId));

	return TRUE;
	
}


DWORD WINAPI RecvData( VOID * dummy )					
{
	DWORD	dwByte;
	char	szRecv[2];
	char	recv_ch;
	char    Buffer[5];
	int     buf_cnt = 0;
	int     ch;
	int     i;
	int		nRet;
	HWND    hListCrl;
	char tmp[25];
	char    str[50];
	struct coordinate  * data;
	int cnt = 0;

	LVITEM LI;
	LI.mask = LVIF_TEXT;

	hListCrl = GetDlgItem(hWnd, IDC_LIST_CONTROL);	

	while(1)
	{		
		nRet = ReadFile(hFile, szRecv, 1, &dwByte, 0);
		if(dwByte == 1)
		{
			recv_ch = szRecv[0];
			szRecv[1] = 0;	
		//	MessageBox(hWnd,(LPCSTR)szRecv, (LPCSTR)"Error", MB_OK);
			if(recv_ch == '#' )
			{	
				//MessageBox(hWnd,(LPCSTR)szRecv, (LPCSTR)"Error", MB_OK);
				//ReadFile(hFile, szRecv, 1, &dwByte, 0);
				//recv_ch = szRecv[0];				
				//szRecv[1] = 0;		
				break;										

			}
		}
	}


	while(243){
		
		if(Sending_flag == 0)
		{			
			CloseHandle(hFile);
			return 0;		
		}
		// 한 문자 수신
		nRet = ReadFile(hFile, szRecv, 1, &dwByte, 0);
		
		// ReadFile()은 성공하면 0이외를 반환, 타임아웃도 성공
		if(dwByte == 1)
		{	
			ch = szRecv[0];		
			Buffer[buf_cnt] = ch;
			

			if(buf_cnt == 4)
			{		
					int val;

					Buffer[4] = 0;

					sprintf(tmp, "%s", Buffer);
					LI.pszText = tmp;

					SetDlgItemText(hWnd, IDC_EDIT1, tmp);

					val = atoi(Buffer);

					sneding_value(val);
					
					cnt++;
					
					buf_cnt = 0;
					for(i = 0 ; i < 5 ; i++)
					{
						Buffer[i] = NULL;
					}
		
			} else
				buf_cnt++;			
		}	
		
	}
	return 0;
}