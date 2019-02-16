﻿using System;

namespace MessageTracker
{
    public static class Helper
    {
        public static void WriteIntro()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(@"-------------------------------
                                    
$$\      $$\                                                                   $$$$$$$\                      
$$$\    $$$ |                                                                  $$  __$$\                     
$$$$\  $$$$ | $$$$$$\   $$$$$$$\  $$$$$$$\  $$$$$$\   $$$$$$\   $$$$$$\        $$ |  $$ |$$\   $$\  $$$$$$$\ 
$$\$$\$$ $$ |$$  __$$\ $$  _____|$$  _____| \____$$\ $$  __$$\ $$  __$$\       $$$$$$$\ |$$ |  $$ |$$  _____|
$$ \$$$  $$ |$$$$$$$$ |\$$$$$$\  \$$$$$$\   $$$$$$$ |$$ /  $$ |$$$$$$$$ |      $$  __$$\ $$ |  $$ |\$$$$$$\  
$$ |\$  /$$ |$$   ____| \____$$\  \____$$\ $$  __$$ |$$ |  $$ |$$   ____|      $$ |  $$ |$$ |  $$ | \____$$\ 
$$ | \_/ $$ |\$$$$$$$\ $$$$$$$  |$$$$$$$  |\$$$$$$$ |\$$$$$$$ |\$$$$$$$\       $$$$$$$  |\$$$$$$  |$$$$$$$  |
\__|     \__| \_______|\_______/ \_______/  \_______| \____$$ | \_______|      \_______/  \______/ \_______/ 
                                                     $$\   $$ |                                              
                                                     \$$$$$$  |                                              
                                                      \______/                                               



$$$$$$$$\ $$$$$$\ $$$$$$$$\  $$$$$$\         $$$$$$\   $$$$$$\  $$\   $$\ $$\   $$\ $$$$$$$$\ 
$$  _____|\_$$  _|$$  _____|$$  __$$\       $$  __$$\ $$  __$$\ $$ |  $$ |$$ |  $$ |$$  _____|
$$ |        $$ |  $$ |      $$ /  $$ |      $$ /  $$ |$$ /  $$ |$$ |  $$ |$$ |  $$ |$$ |      
$$$$$\      $$ |  $$$$$\    $$ |  $$ |      $$ |  $$ |$$ |  $$ |$$ |  $$ |$$ |  $$ |$$$$$\    
$$  __|     $$ |  $$  __|   $$ |  $$ |      $$ |  $$ |$$ |  $$ |$$ |  $$ |$$ |  $$ |$$  __|   
$$ |        $$ |  $$ |      $$ |  $$ |      $$ $$\$$ |$$ |  $$ |$$ |  $$ |$$ |  $$ |$$ |      
$$ |      $$$$$$\ $$ |       $$$$$$  |      \$$$$$$ /  $$$$$$  |\$$$$$$  |\$$$$$$  |$$$$$$$$\ 
\__|      \______|\__|       \______/        \___$$$\  \______/  \______/  \______/ \________|
                                                 \___|                                        
                                                                                              
                                                                                              


Press 'Q' to exit
                                    -------------------------------
                                    ");

            Console.ResetColor();
        }
    }
}