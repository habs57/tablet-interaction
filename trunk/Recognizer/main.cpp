#include "resource.h"
#include "main.h"
#include <commctrl.h>



int under_weak;
int weak_strong;
int strong_hard;
int over_hard;


int WINAPI WinMain(HINSTANCE hInstance, HINSTANCE hPrevInstance,
				   PSTR szCmdLine, int iCmdShow)
{
	DialogBox(hInstance,MAKEINTRESOURCE(IDD_DIALOG1),NULL,WndProc);
	return 0;
} // 다이얼 로그의 생성.


BOOL CALLBACK WndProc(HWND hwnd, UINT message, WPARAM wParam, LPARAM lParam)
{
	hWnd = hwnd;    
	// 리턴 받은 메인 다이얼로그의 핸들 값을 전역 변수에 넣어 놓는다.
	// 이후 대부분의 함수에서 hwnd보다 hWnd를 사용한다.

	hCombo = GetDlgItem(hWnd, IDC_BOUDRATE);
	hListCrl = GetDlgItem(hWnd, IDC_LIST_CONTROL);
	hListCrl_filter = GetDlgItem(hWnd, IDC_LIST_CONTROL2);
	hCombo2 = GetDlgItem(hWnd, IDC_COM_PORT);
	
	// 몇개의 컨트롤들의 핸들값을 얻어 놓는다.
	
	switch (message)
	{
	case WM_INITDIALOG:
		{			
			init_controls(hCombo, hCombo2, hListCrl, hListCrl_filter);
			socket_init();
		}   // 사용되는 몇몇 컨트롤을 초기화 한다. main.cpp에 구현되어 있음.
		return TRUE;
	case WM_COMMAND:
		switch(LOWORD(wParam))
		{
		case IDOK:
		case IDCANCEL:
			fcloseall();
			EndDialog(hwnd,IDOK);   // OK, Cancel을 누르면 프로그램 종료.
			break;
		case IDC_OPEN:
			{
				Sending_flag = 1;
				Open();				
			}                       // 세 함수의 구현은 serial_comm.cpp
			break;
		case IDC_CLOSE:
			{
	
			}
			break;

		case IDC_BUTTON_SET:            
			{    
				char pEditBox[24];

				GetDlgItemText(hWnd, IDC_EDIT_NONE, pEditBox, 24);
				under_weak = atoi(pEditBox);

				GetDlgItemText(hWnd, IDC_EDIT_NONE_WEAK, pEditBox, 24);
				weak_strong = atoi(pEditBox);

				GetDlgItemText(hWnd, IDC_EDIT_WEAK_STRONG, pEditBox, 24);
				strong_hard = atoi(pEditBox);

				GetDlgItemText(hWnd, IDC_EDIT_OVER_STRONG, pEditBox, 24);
				over_hard = atoi(pEditBox);

			}
			break;		
    
		case IDC_BUTTON_SAVE:       
			{				       

			}                      

		}
		return TRUE;
	}
	return FALSE;
}

void init_controls(HWND hCombo ,HWND hCombo2, HWND hListCrl, HWND hListCrl_filter)
{
	int i = 0;

	LVCOLUMN COL;
	char temp[256];
	sprintf(temp, "coordinate_1.dat");
	SetDlgItemText(hWnd, IDC_EDIT_FILE, temp);
	for(i = 0 ; i < 4 ; i++)
	{
		SendMessage(hCombo,CB_ADDSTRING,0,(LPARAM)Items[i]);
	}
	
	SendMessage(hCombo, CB_SETCURSEL,3,0);

	for(i = 0 ; i < 19 ; i++)
	{
		SendMessage(hCombo2,CB_ADDSTRING,2,(LPARAM)Ports[i]);
	}
	
	SendMessage(hCombo2, CB_SETCURSEL,0,0);


	SetDlgItemText(hWnd, IDC_EDIT_NONE, "250");
	SetDlgItemText(hWnd, IDC_EDIT_NONE_WEAK, "600");
	SetDlgItemText(hWnd, IDC_EDIT_WEAK_STRONG, "760");
	SetDlgItemText(hWnd, IDC_EDIT_OVER_STRONG, "800");

	char pEditBox[24];

	GetDlgItemText(hWnd, IDC_EDIT_NONE, pEditBox, 24);
	under_weak = atoi(pEditBox);

	GetDlgItemText(hWnd, IDC_EDIT_NONE_WEAK, pEditBox, 24);
	weak_strong = atoi(pEditBox);

	GetDlgItemText(hWnd, IDC_EDIT_WEAK_STRONG, pEditBox, 24);
	strong_hard = atoi(pEditBox);

	GetDlgItemText(hWnd, IDC_EDIT_OVER_STRONG, pEditBox, 24);
	over_hard = atoi(pEditBox);

}