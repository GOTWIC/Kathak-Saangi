//
//  Octopad.swift
//  Tabla App
//
//  Created by Swagnik Roychoudhury on 9/5/21.
//

import SwiftUI
import AVKit
import Foundation


struct Octapad: View {

    
    var videoNames = ["Composition 1", "Composition 2", "Composition 3"]
    var videoIDs = ["g_iIpYxEwUs", "C_wS4L4a7RI", "1KPB0Vy2dKo"]
    
    
    @StateObject var modifiers = ScreenModifiers()
    
    
    var body: some View
    {
        
        ZStack
        {
            LinearGradient(gradient: Gradient(colors: [Color.orange1, Color.grey2, Color.grey2]), startPoint: .top, endPoint: .bottom).ignoresSafeArea()
            VStack
            {
                Spacer()
                    .frame(height: modifiers.ht * 30)
                Image("Octapad")
                    .resizable()
                    .frame(width: modifiers.ht * 300, height: modifiers.ht * 195)
                    .border(Color.black, width: 5)
                
                Spacer()
                    .frame(height: modifiers.ht * 30)
                
                Text("Octapad")
                    .foregroundColor(.gold2)
                    .font(.system(size: modifiers.wt * 24, weight: .bold))
                    .frame(width: modifiers.wt * 100, height: modifiers.wt * 25, alignment: .center)
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
                        
                        Text("Custom compositions created via the Octopad for kathakars' own choreographies.")
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
                        Text("Compositions")
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
                            
                            VideoList(videoID: videoIDs[video], videoName: videoNames[video])
                            
                        }
                    }
                }
            }
            
            
            
        }//.navigationBarTitle("Practice", displayMode: .inline)
        
        
        
        
        
        
        
        
    }
    
}

struct Octopad_Previews: PreviewProvider {
    
    static var previews: some View {
        
        Group {
            
            Octapad().preferredColorScheme(/*@START_MENU_TOKEN@*/.dark/*@END_MENU_TOKEN@*/)
            
        }
        
    }
    
}


