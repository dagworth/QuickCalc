using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickCalc.Models {
    internal static class Calculation {
        private static Dictionary<char, string> vars = [];

        //almost all useless, i wanna find a way to remove these cus ugly
        private static readonly char[] numbers = ['1','2','3','4','5','6','7','8','9','0','.'];
        private static readonly char[] valid_start = ['1','2','3','4','5','6','7','8','9','0','.', '('];
        private static readonly char[] valid_end = ['1','2','3','4','5','6','7','8','9','0',')'];
        
        //gets the distance of the index of the next number from the current position
        private static int LengthNextNum(string equation, int start){
            for(int i = start; i < equation.Length; i++){
                char c = equation[i];
        
                //edge cases
                if(i == start){
                    if(c == '-'){ //in case of 5*-7, negative right after operation, it will still count as a number
                        continue;
                    } else if (vars.ContainsKey(c)){ //if its a variable, then only have the variable be the number
                        return 1;
                    }
                }

                if(!numbers.Contains(c)){ //if the currenet variable isnt a number, return how long it has been while it was a number
                    return i-start;
                }
            }
            return equation.Length-start; //returns the end of the equation if we cant find a non-number char
        }

        //makes the str into a number, takes into account variables
        private static double MakeNum(string str){
            if(vars.ContainsKey(str[0])) str = vars[str[0]];
            return double.Parse(str);
        }

        //does out operations that it is given in the equation
        private static void DoOperation(List<string> equation_list, string[] operations){
            for(int i = 0; i<equation_list.Count; i++){
                string str = equation_list[i];
                if(operations.Contains(str)){
                    double value = 0;

                    //handles the first negative
                    //there has to be a better place to write this
                    if(i == 0 && str.Equals("-") && operations.Contains("-")){
                        value = -MakeNum(equation_list[i+1]);
                        equation_list.RemoveRange(0,2);
                        equation_list.Insert(0,value.ToString());
                        continue;
                    }

                    double last_num = MakeNum(equation_list[i-1]);
                    double next_num = MakeNum(equation_list[i+1]);

                    if(str.Equals("^")){
                        value = Math.Pow(last_num,next_num);
                    } else if (str.Equals("*")){
                        value = last_num*next_num;
                    } else if (str.Equals("/")){
                        value = last_num/next_num;
                    } else if (str.Equals("+")){
                        value = last_num+next_num;
                    } else if (str.Equals("-")){
                        value = last_num-next_num;
                    }

                    equation_list.RemoveRange(i-1,3);
                    equation_list.Insert(i-1,value.ToString());

                    i-=1;
                }
            }
        }

        //this will always evaluate equation_list into a single number
        private static string DoOut(List<string> equation_list){
            DoOperation(equation_list,["^"]);
            DoOperation(equation_list,["*","/"]);
            DoOperation(equation_list,["+","-"]);
            return MakeNum(equation_list[0]).ToString();
            //equation_list[0]; doesn't work because it doesn't account for equation_list being one variable
        }

        //separates each line into tokens to use in calculation
        //example:
        //-(2(3+2))(-12-2)^2
        //- ( 2 * ( 3 + 2 ) ) * ( -12 - 2 ) ^ 2
        private static List<string> SeparateLine(string equation){
            List<string> equation_list = new();
            bool last_is_num = false;

            for(int i = 0; i < equation.Length; i++){
                char c = equation[i];

                //for operations and parentheses
                //taking into account when theres a negative number in the middle of the equation
                if(new char[]{'+','*','/','(',')','^'}.Contains(c) || (c == '-' && (last_is_num || i == 0))){

                    if(c == '(' && last_is_num) equation_list.Add("*"); // '(' is the only character here that will result in a number facing left

                    equation_list.Add(c.ToString());
                    last_is_num = c == ')'; // ')' is the only character here that will result in a number facing right

                } else {
                    if(last_is_num) equation_list.Add("*"); //this adds * for variables that need it
                    last_is_num = true;

                    int len = LengthNextNum(equation,i);
                    equation_list.Add(equation.Substring(i,len));
                    i+=len-1;
                }
            }

            Console.WriteLine(String.Join(" ",equation_list.ToArray()));

            return equation_list;
        }

        private static string IsValid(string equation){
            if(!valid_start.Contains(equation[0]) && equation[0] != '-' && !vars.ContainsKey(equation[0])) return "error: not a valid start";
            if(!valid_end.Contains(equation[equation.Length-1]) && !vars.ContainsKey(equation[equation.Length-1])) return "error: not a valid end";

            for(int i = 0; i < equation.Length; i++){
                //error finding that i still need to figure out
                //donno if i should try doing the check only one time or not
            }

            return "";
        }


        //takes in a single line and evaluates it
        private static string SolveLine(string equation) {

            //check if the equation is valid
            string valid_check = IsValid(equation);
            if(valid_check.Length != 0) return valid_check;

            List<string> equation_list = SeparateLine(equation);

            //does out the equation in parentheses, given that its a polynomial
            List<int> open_parentheses = new();
            for(int i = 0; i < equation_list.Count; i++){
                string c = equation_list[i];
                if(c.Equals("(")){
                    open_parentheses.Add(i);
                } else if (c.Equals(")")){
                    //get the last open parentheses and removes it from the list
                    //is user didnt close the ), put a '(' at the front
                    if(open_parentheses.Count == 0){ open_parentheses.Insert(0,0); equation_list.Insert(0,"("); i++; };
                    int last = open_parentheses[open_parentheses.Count - 1];
                    open_parentheses.RemoveAt(open_parentheses.Count - 1);

                    //do everything in () and replaces it in equation_list
                    List<string> range = equation_list.GetRange(last+1,i-last-1);

                    string range_valid_check = IsValid(String.Join("",equation_list.ToArray()));
                    if(range_valid_check.Length != 0) return range_valid_check;

                    //ADD ERROR CHECKS HERE

                    string result = DoOut(range);
                    equation_list.RemoveRange(last,i-last+1);
                    equation_list.Insert(last,result);

                    i = last;
                }

                //if user didnt close the '(', put a ')' at the end
                if(i+1 == equation_list.Count && open_parentheses.Count > 0){ equation_list.Add(")"); i--; }
            }

            return DoOut(equation_list);
        }

        //the function that solves stuff
        public static string Solve(string text){
            vars.Clear();
            string[] lines = text.Replace(" ", "").ToLower().Split(['\n','\r']);
            string response = "";
            for(int line_index = 0; line_index < lines.Length; line_index++){
                if(line_index % 2 != 0){ continue; } //only in visual studio cus its weird and it doubles the \n

                string line = lines[line_index];
                string value = "";

                if(line.Length > 2 && line[1] == '='){
                    value = SolveLine(line.Substring(2));
                    if (value.StartsWith("error")) vars[line[0]] = value; //error catcher so variables wont be assigned error messages
                } else if (line.Length > 0){
                    value = SolveLine(line);
                }
                response+=value+"\n";
            }
            return response;
        }
    }
}