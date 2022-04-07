//
//  Riyaz.swift
//  Tabla App
//
//  Created by Swagnik Roychoudhury on 9/5/21.
//
import SwiftUI
import AVKit
import Foundation


struct Riyaz: View {
    
    var videoIDs = ["9oQ1I6Gd8WA", "8UbqVNYDsU8", "iAc9FZmE1v0", "_61oloq9hUQ", "rCCc6Xt78pE", "KsNAbWwF4iM", "zM86SZeJm7Y", "iBZyuNfCLvs", "8epz40QiGU4", "AmPQK48SexI", "4ndvTM_xBLM", "D5WlBh4HxVw", "miI3va_HeyM", "AgitzxfwuzE", "76g-_6uKJJU", "uH-md4Jklk4", "e9y9mM1D694", "DX3cTvkzIDw", "-omto_-uRz0", "O-kdc2asQHI"]
    
    var videoNames = ["2 Beat", "3 Beat", "4 Beat ", "100 BPM", "150 BPM", "220 BPM", "300 BPM", "350 BPM ", "440 BPM", "520 BPM", "110 BPM", "130 BPM", "175 BPM", "1 Beat - 70 BPM", "1 Beat - 80 BPM", "1 Beat - 90 BPM", "2 Beat - 70 BPM", "2 Beat - 80 BPM", "3 Beat", "5 Beat"]
    
    
    @StateObject var modifiers = ScreenModifiers()
    
    
    var body: some View
    {
        
        
        
        ZStack
        {
            LinearGradient(gradient: Gradient(colors: [Color.red3, Color.grey2, Color.grey2]), startPoint: .top, endPoint: .bottom).ignoresSafeArea()
            VStack
            {
                
                Spacer()
                    .frame(height: modifiers.wt * 30)
                
                Image("Tabla Image 1")
                    .resizable()
                    .frame(width: modifiers.ht * 300, height: modifiers.ht * 187)
                    .border(Color.black, width: 5)
                
                Spacer()
                    .frame(height: modifiers.ht * 30)
                
                Text("Riyaz")
                    .foregroundColor(.gold2)
                    .font(.system(size: modifiers.wt * 24, weight: .bold))
                    .frame(width: modifiers.wt * 120, height: modifiers.ht * 25, alignment: .center)
                    .padding()
                    .background(Color.grey2)
                    .border(Color.gold2, width: /*@START_MENU_TOKEN@*/1/*@END_MENU_TOKEN@*/)
                    //.cornerRadius(20)
                    
                    //Text("Subtitle")
                    .foregroundColor(.white)
                Spacer()
                
            }
            
            ScrollView
            {
                
                
                
                VStack(spacing:0)
                {
                    HStack
                    {
                        Spacer()
                            .frame(height: modifiers.ht * 360)
                            .background(LinearGradient(gradient: Gradient(colors: [Color.clear, Color.clear, Color.clear, Color.clear, Color.grey2]), startPoint: .top, endPoint: .bottom))
                    }
                
                    
                    HStack
                    {
                        Text("Purpose")
                            .fontWeight(.bold)
                            .font(.system(size: modifiers.wt * 30))
                        
                        Spacer()
                        
                    }
                    .padding(10)
                    .background(Color.grey2)
                    .foregroundColor(.gold2)
                    .border(width: modifiers.wt * 2, edges: [.bottom], color: .gold2)
                    .padding(modifiers.wt * 20)
                    .background(Color.grey2)
                    
                    HStack{
                        
                        Text("Use these practice audios for your daily riyaz of hand-movements, footwork, and circles. Demonstrations of hastak, circle, and footwork can be found in the OUR GURUS page.")
                            .fixedSize(horizontal: false, vertical: true)
                            .multilineTextAlignment(.center)
                            .frame(width: modifiers.wt * 300)
                            .font(.system(size: modifiers.wt * 16))
                            .foregroundColor(.gold2)
                            .padding(modifiers.wt * 20)
                            .background(Color.black1)
                            .cornerRadius(15)
                        
                    }
                    .frame(width: modifiers.wt * 390)
                    .background(Color.grey2)
                    
                    Group{
                        HStack
                        {
                            Text("Hastak")
                                .fontWeight(.bold)
                                .font(.system(size: modifiers.wt * 30))
                            
                            Spacer()
                            
                        }
                        .padding(10)
                        .background(Color.grey2)
                        .foregroundColor(.gold2)
                        .border(width: modifiers.ht * 2, edges: [.bottom], color: .gold2)
                        .padding(modifiers.wt * 20)
                        .background(Color.grey2)
                        
                        
                        ForEach(0..<3){video in
                            VStack{

                                VideoList(videoID: videoIDs[video], videoName: videoNames[video])
                            }
                        }
                    }
                    
                    
                    Group{
                        HStack
                        {
                            Text("Taatkar")
                                .fontWeight(.bold)
                                .font(.system(size: modifiers.wt * 30))
                            
                            Spacer()
                            
                        }
                        .padding(10)
                        .background(Color.grey2)
                        .foregroundColor(.gold2)
                        .border(width: modifiers.ht * 2, edges: [.bottom], color: .gold2)
                        .padding(modifiers.wt * 20)
                        .background(Color.grey2)
                        
                        
                        ForEach(0..<7){video in
                            VStack{
                                
                                VideoList(videoID: videoIDs[video + 3], videoName: videoNames[video + 3])
                            }
                        }
                    }
                    
                    
                    Group{
                        HStack
                        {
                            Text("Takita Takita Dhin")
                                .fontWeight(.bold)
                                .font(.system(size: modifiers.wt * 30))
                            
                            Spacer()
                            
                        }
                        .padding(10)
                        .background(Color.grey2)
                        .foregroundColor(.gold2)
                        .border(width: modifiers.wt * 2, edges: [.bottom], color: .gold2)
                        .padding(modifiers.wt * 20)
                        .background(Color.grey2)
                        
                        
                        ForEach(0..<3){video in
                            VStack{
                                
                                VideoList(videoID: videoIDs[video + 10], videoName: videoNames[video + 10])
                            }
                        }
                    }
                    
                    
                    Group{
                        HStack
                        {
                            Text("Chakkar (Circle)")
                                .fontWeight(.bold)
                                .font(.system(size: modifiers.wt * 30))
                            
                            Spacer()
                            
                        }
                        .padding(10)
                        .background(Color.grey2)
                        .foregroundColor(.gold2)
                        .border(width: modifiers.wt * 2, edges: [.bottom], color: .gold2)
                        .padding(modifiers.wt * 20)
                        .background(Color.grey2)
                        
                        
                        ForEach(0..<7){video in
                            VStack{
                                
                                VideoList(videoID: videoIDs[video + 13], videoName: videoNames[video + 13])
                            }
                        }
                    }
                    
                }
            }
            
            

            
            
        }//.navigationBarTitle("Practice", displayMode: .inline)
        
        
        
        
        
        
        
        
    }
    
}

struct Riyaz_Previews: PreviewProvider {
    
    static var previews: some View {
        
        Group {
            Riyaz().preferredColorScheme(.dark)
            
            
        }
        
    }
    
}
