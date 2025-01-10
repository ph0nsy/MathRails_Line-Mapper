using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class TestFunctionCoord
{
    // A Test behaves as an ordinary method
    [Test]
    public void TestFunctionCoordSimplePasses()
    {
        string funcToTest = "y=x";
        for(int i = -10; i < 11; i++){
            Assert.AreEqual(CalculateStringAtX(i,funcToTest), i);
        }
    }

    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    [UnityTest]
    public IEnumerator TestFunctionCoordWithEnumeratorPasses()
    {
        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        yield return null;
    }

    double CalculateStringAtN(double x, string equation, bool atX)
    { 
        double value = 0;
        string tempEq = equation.Remove(0,2);
        tempEq = atX ? tempEq.Replace('x', x.ToString()) : tempEq.Replace('y', x.ToString());
        char[] charEq = tempEq.ToCharArray();
        for(int i = 0; i < charEq.Length; i++){
            if (i == 0)
            {
                if(charEq[0] == '-'){
                    if(charEq[i+1] == 'x'){
                        if(i+2 < charEq.Length && charEq[i+2] == '^'){
                                value -= (double)Mathf.Pow((float)x,2.0f);
                                i++;
                            }
                        else
                            value -= x;
                    }
                    else 
                        value = -1 * Convert.ToDouble(charEq[1].ToString());
                    i++;
                }
                else if (charEq[0] == 'x' && charEq[i+1] == '^'){
                    value = (double)Mathf.Pow((float)x,2.0f);
                    i++;
                }
                else
                    value = charEq[0] == 'x' ? x : Convert.ToDouble(charEq[0].ToString());
            }
            else if (charEq[i] == '+' || charEq[i] == '-' || charEq[i] == '*' || charEq[i] == '/') 
            {

                if(charEq[i] == '+'){
                    if(charEq[i+1] == 'x'){
                        if(i+2 < charEq.Length && charEq[i+2] == '^'){
                            value += (double)Mathf.Pow((float)x,2.0f);
                            i++;
                        }
                        else
                            value += x;
                    } 
                    else if(charEq[i+1] == '-' && charEq[i+2] == 'x'){
                        if(i+3 < charEq.Length && charEq[i+3] == '^'){
                            value -= (double)Mathf.Pow((float)x,2.0f);
                            i++;
                        }
                        else
                            value -= x;
                        i++;
                    }
                    else 
                        value += Convert.ToDouble(charEq[i+1].ToString());
                    i++;
                }

                if(charEq[i] == '-'){
                    if(charEq[i+1] == 'x'){
                        if(i+2 < charEq.Length && charEq[i+2] == '^'){
                            value -= (double)Mathf.Pow((float)x,2.0f);
                            i++;
                        }
                        else
                            value -= x;
                    } 
                    else if(charEq[i+1] == '-' && charEq[i+2] == 'x'){
                        if(i+3 < charEq.Length && charEq[i+3] == '^'){
                            value += (double)Mathf.Pow((float)x,2.0f);
                            i++;
                        }
                        else
                            value += x;
                        i++;
                    }
                    else 
                        value -= Convert.ToDouble(charEq[i+1].ToString());
                    i++;
                }
                
                if(charEq[i] == '*'){
                    if(charEq[i+1] == 'x'){
                        if(i+2 < charEq.Length && charEq[i+2] == '^'){
                            value *= (double)Mathf.Pow((float)x,2.0f);
                            i++;
                        }
                        else
                            value *= x;
                    } 
                    else if(charEq[i+1] == '-' && charEq[i+2] == 'x'){
                        if(i+3 < charEq.Length && charEq[i+3] == '^'){
                            value *= -(double)Mathf.Pow((float)x,2.0f);
                            i++;
                        }
                        else
                            value *= -x;
                        i++;
                    }
                    else 
                        value *= Convert.ToDouble(charEq[i+1].ToString());
                    i++;
                }
                
                if(charEq[i] == '/'){
                    if(charEq[i+1] == 'x'){
                        if(i+2 < charEq.Length && charEq[i+2] == '^'){
                            value /= (double)Mathf.Pow((float)x,2.0f);
                            i++;
                        }
                        else
                            value /= x;
                    } 
                    else if(charEq[i+1] == '-' && charEq[i+2] == 'x'){
                        if(i+3 < charEq.Length && charEq[i+3] == '^'){
                            value /= -(double)Mathf.Pow((float)x,2.0f);
                            i++;
                        }
                        else
                            value /= -x;
                        i++;
                    }
                    else 
                        value /= Convert.ToDouble(charEq[i+1].ToString());
                    i++;
                }       
            }
            
        }
        return value; 
    }
}
