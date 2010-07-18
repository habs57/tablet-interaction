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
} // ���̾� �α��� ����.


BOOL CALLBACK WndProc(HWND hwnd, UINT message, WPARAM wParam, LPARAM lParam)
{
	hWnd = hwnd;    
	// ���� ���� ���� ���̾�α��� �ڵ� ���� ���� ������ �־� ���´�.
	// ���� ��κ��� �Լ����� hwnd���� hWnd�� ����Ѵ�.

	hCombo = GetDlgItem(hWnd, IDC_BOUDRATE);
	hListCrl = GetDlgItem(hWnd, IDC_LIST_CONTROL);
	hListCrl_filter = GetDlgItem(hWnd, IDC_LIST_CONTROL2);
	hCombo2 = GetDlgItem(hWnd, IDC_COM_PORT);
	
	// ��� ��Ʈ�ѵ��� �ڵ鰪�� ��� ���´�.
	
	switch (message)
	{
	case WM_INITDIALOG:
		{			
			init_controls(hCombo, hCombo2, hListCrl, hListCrl_filter);
			socket_init();
		}   // ���Ǵ� ��� ��Ʈ���� �ʱ�ȭ �Ѵ�. main.cpp�� �����Ǿ� ����.
		return TRUE;
	case WM_COMMAND:
		switch(LOWORD(wParam))
		{
		case IDOK:
		case IDCANCEL:
			fcloseall();
			EndDialog(hwnd,IDOK);   // OK, Cancel�� ������ ���α׷� ����.
			break;
		case IDC_OPEN:
			{
				Sending_flag = 1;
				Open();				
			}                       // �� �Լ��� ������ serial_comm.cpp
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