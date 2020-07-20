using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConvertNumToString
{
    class Program
    {
        static void Main(string[] args)
        {
            long num = 230109384;
            Console.WriteLine(convertNumToString(num));
            Console.ReadLine();
        }

        //This is the method that converts the entire number to the string version.
        private static string convertNumToString(long num)
        {
            //remove any leading 0s
            string strNum = num.ToString().TrimStart('0');
            //if there were only 0s in the number, return zero
            if (strNum.Length == 0) return "zero";

            //get the number of digits so we know how many times to loop
            int numOfDigits = strNum.Length;
            //represents the current number we are looking at, starting from the right
            long currNum;
            //This is the final result
            string res = "";
            //i represents the position of the current number (1's place, 10's place, 100's place, ...)
            for(int i = 0; i < numOfDigits; i++)
            {
                //get the right most number
                currNum = num % 10;

                //get name of the current number based on its current position
                string tempRes = getNameOfPosition(strNum, i, currNum);

                //append the partial result to our final string.
                res = tempRes == "" ? res : tempRes + " " + res;

                //remove the right most number
                num = num / 10;
            }
            return res;
        }

        //This method returns the name for the current number based on its position.
        //strNum is the original number converted to a string, this makes it easy for us to get specific characters
        //position represents the weight of the current number (1's place, 10's place, ...)
        //num is the current number we are looking at
        private static string getNameOfPosition(string strNum, int position, long num)
        {
            //this is the index in the original number where num is found
            int index = strNum.Length - 1 - position;
            //we use this to hold intermediate results
            string temp;
            //in some cases we want to know what the next number is.
            int nextNum = (index - 1 >= 0) ? int.Parse(strNum[index - 1].ToString()) : -1;
            //this is used for the special case where we are looking at thousands, millions, or billions.
            //it says if the current number is 0 and there are non zero numbers after it, we want to force print the units (thousands, millions, billions)
            bool forceUnit = (num == 0) && ((index - 1 >= 0) && (strNum[index - 1] != '0') || (index - 2 >= 0) && (strNum[index - 2] != '0'));
            switch (position)
            {
                case 0:
                    //this is the ones place
                    if(nextNum == 1)
                    {
                        //special case, 12 = twelve
                        return convertTeens(num);
                    }
                    else
                    {
                        return convertSingleNumToChar(num);
                    }
                case 10:
                case 7:
                case 4:
                case 1:
                    //this is the tens, ten thousands, ten hundren thousands, ... place
                    return convertTens(num);
                case 11:
                case 8:
                case 5:
                case 2:
                    //this is the hundreds, hundred thousands, hundren millions, ... place
                    temp = convertSingleNumToChar(num);
                    return temp != ""  ? convertSingleNumToChar(num) + " hundred": "";
                case 3:
                    //this is the thousands place
                    return thousandsMillionsBillionsHelper("thousand", num, nextNum, forceUnit);
                case 6:
                    //this is the millions place
                    return thousandsMillionsBillionsHelper("million", num, nextNum, forceUnit);
                case 9:
                    //this is the billions place
                    return thousandsMillionsBillionsHelper("billion", num, nextNum, forceUnit);
            }
            
            return "";
        }

        private static string thousandsMillionsBillionsHelper(string unit, long num, int nextNum, bool forceUnit)
        {
            if (nextNum == 1)
            {
                //special case, 12000000000 = twelve billion
                return convertTeens(num) + " " + unit;
            }
            //convert the single number to its word form
            string temp = convertSingleNumToChar(num);
            //if the current number is 0, but there is a non zero number two numbers ahead, we still want to  print the unit.
            if (num == 0 && forceUnit) return unit;
            //in this case the number is non zero, so we print the word form of the number and the unit (thousand, million, billion)
            else if (temp != "") return convertSingleNumToChar(num) + " " + unit;
            else return "";
        }

        private static string convertTens(long num)
        {
            switch (num)
            {
                case 2:
                    return "twenty";
                case 3:
                    return "thirty";
                case 4:
                    return "fourty";
                case 5:
                    return "fifty";
                case 6:
                    return "sixty";
                case 7:
                    return "seventy";
                case 8:
                    return "eighty";
                case 9:
                    return "ninety";
                default:
                    return "";
            }
        }

        private static string convertTeens(long num)
        {
            switch (num)
            {
                case 0:
                    return "ten";
                case 1:
                    return "eleven";
                case 2:
                    return "twelve";
                case 3:
                    return "thirteen";
                case 4:
                    return "fourteen";
                case 5:
                    return "fifteen";
                case 6:
                    return "sixteen";
                case 7:
                    return "seventeen";
                case 8:
                    return "eighteen";
                case 9:
                    return "nineteen";
                default:
                    return "";
            }
        }

        private static string convertSingleNumToChar(long num)
        {
            switch(num)
            {
                case 1:
                    return "one";
                case 2:
                    return "two";
                case 3:
                    return "three";
                case 4:
                    return "four";
                case 5:
                    return "five";
                case 6:
                    return "six";
                case 7:
                    return "seven";
                case 8:
                    return "eight";
                case 9:
                    return "nine";
                default:
                    return "";
            }
        }
    }
}
