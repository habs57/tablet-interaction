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


	// ����Ʈ ����
	hFile = CreateFile(str,
				GENERIC_READ | GENERIC_WRITE,
				0,				// �����
				0,				// ��ť��Ƽ �Ӽ�:������
				OPEN_EXISTING,	// ���� ���� ����
				0, 0 );			// �Ӽ�, ���÷���Ʈ
	
	if(hFile == INVALID_HANDLE_VALUE)
		MessageBox(0, "Can't Open COM port!","Caution", MB_ICONERROR);
	
	DCB dcb;
	
	// ������ ��� ����̽��� ���� DCB ���� ���
	// DCB : Device Control Block ����̽� ���� ��
	GetCommState(hFile , &dcb);			
											
	// ������ ������ ���� �ڵ� �߰�
	//		:
	
	// DCB�� ������ ���� ��� ����̽� ����
	// �ϵ����� ���� ���� �ʱ�ȭ
	SetCommState(hFile , &dcb);			
	
//	wsprintf(szBuffer, "BaudRate %d : ByteSize %d", dcb.BaudRate, dcb.ByteSize);
	
	// ���̾˷α׹ڽ� ĸ�ǿ� BaudRate:ByteSize ������ ǥ��
//	SetWindowText(hWnd, szBuffer);

	COMMTIMEOUTS	cTimeout;

	// ���� �������� Ÿ�Ӿƿ� �ڷ� ���
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
		// �� ���� ����
		nRet = ReadFile(hFile, szRecv, 1, &dwByte, 0);
		
		// ReadFile()�� �����ϸ� 0�ܸ̿� ��ȯ, Ÿ�Ӿƿ��� ����
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