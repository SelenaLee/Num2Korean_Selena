using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace practice_0109
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        //인트 범위의 숫자를 입력받으면 한글로 출력해주는 프로그램
 
        public MainPage()
        {
            this.InitializeComponent();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            string result = "";
            int inputNumFromUser;
            if (!String.IsNullOrWhiteSpace(inputNumber.Text) && int.TryParse(inputNumber.Text, out inputNumFromUser))
            // 값을 입력하지 않거나, 입력받은 값을 숫자로 변환하지 못하는 경우를 검사
            {
                if (inputNumFromUser == 0)
                {
                    printNum.Text = "영";
                    return;
                }
                else if (inputNumFromUser < 0) // 입력받은 값이 마이너스인 경우
                {
                    inputNumFromUser *= -1;
                    result += "마이너스 ";
                }
                string tmp = inputNumFromUser.ToString();// 입력받은 값의 절댓값 임시 저장.

                // 단위, 숫자 배열
                string[] array1 = { "", "만", "억" };
                string[] array2 = { "", "십", "백", "천" };
                string[] array3 = { "", "", "이", "삼", "사", "오", "육", "칠", "팔", "구" };

                int[] beReversedArray = reverseArray(tmp);

                for (int index = beReversedArray.Length - 1; index >= 0; index--)
                {
                    // 이 삼 사 오 육 칠 ...
                    result += array3[beReversedArray[index]];

                    if (index % 4 > 0 && beReversedArray[index] != 0)
                    {
                        result += array2[index % 4]; // 십 백 천
                    }
                    if (index > 3 && index % 4 == 0)
                    {
                        if (beReversedArray[index] == 1) {
                            result += "일";
                        }
                        result += array1[index / 4]; // 만 억 조 경 해
                    }
                }

                if (beReversedArray[0] == 1) {
                    result += "일";
                }
                
            }
            else
            {
                MessageDialog("Please only enter number.");
            }

            printNum.Text = result; // 정상적인 값이 들어와 if문이 돌면 값 출력, 아닌 경우는 입력창 초기화
        }

        private int[] reverseArray(string str) // 배열 뒤집기
        {
            int[] inputArray = new int[str.Length];

            for (int index = 0; index < str.Length; index++)
            {
                inputArray[index] = int.Parse(str[str.Length-1-index].ToString());
            }

            return inputArray;
        }
    
        private async void MessageDialog(String text)
        {

            var dialog = new MessageDialog(text);
            await dialog.ShowAsync();
        }
    }
}
