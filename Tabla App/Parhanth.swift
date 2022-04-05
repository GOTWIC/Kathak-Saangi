//
//  Octopad.swift
//  Tabla App
//
//  Created by Debjani Roychoudhury on 9/5/21.
//

import SwiftUI
import AVKit
import Foundation


struct Padhant: View {

    
    var videoNames = ["Recitation 1", "Recitation 2", "Recitation 3", "Recitation 4", "Recitation 5", "Recitation 6", "Recitation 7", "Recitation 8", "Recitation 9", "Recitation 10"]
    var videoIDs = ["a0BdMT-T5hc", "zjCJ-KLOjMU", "6X1P-v2dfA0", "OaNC7Tde3WE", "veyzoMFXjdg", "BMzTAnrEjyQ", "Er2dFrsKPUc", "R0mpxdPlcSg", "6w1OwR1tjKI", "joSzRJ8fGcU",
      ]
    
    
    @StateObject var modifiers = ScreenModifiers()
    
    
    var body: some View
    {
        
        ZStack
        {
            LinearGradient(gradient: Gradient(colors: [Color.yellow1, Color.grey2, Color.grey2]), startPoint: .top, endPoint: .bottom).ignoresSafeArea()
            VStack
            {
                Spacer()
                    .frame(height: modifiers.ht * 30)
                Image("Padhant")
                    .resizable()
                    .frame(width: modifiers.ht * 300, height: modifiers.ht * 195)
                    .border(Color.black, width: 5)
                
                Spacer()
                    .frame(height: modifiers.ht * 30)
                
                Text("Padhant")
                    .foregroundColor(.gold2)
                    .font(.system(size: modifiers.wt * 24, weight: .bold))
                    .frame(width: modifiers.wt * 130, height: modifiers.wt * 25, alignment: .center)
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
                        
                        Text("Compositions to help you improve your recitation skills and pronounciation of common Kathak and Tabla Bols. Compositions taught by Guru Aniruddha Mukherjee")
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
                    
                    HStack
                    {
                        Text("Recitations")
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
                    
                    
                    ForEach(0..<10){video in
                        VStack{
                            
                            VideoList(videoID: videoIDs[video], videoName: videoNames[video])
                            
                        }
                    }
                }
            }
            
            
            
        }//.navigationBarTitle("Practice", displayMode: .inline)
        
        
        
        
        
        
        
        
    }
    
}

struct Padhant_Previews: PreviewProvider {
    
    static var previews: some View {
        
        Group {
            
            Padhant().preferredColorScheme(/*@START_MENU_TOKEN@*/.dark/*@END_MENU_TOKEN@*/)
            
        }
        
    }
    
}


