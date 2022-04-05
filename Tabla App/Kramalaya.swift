//
//  KramalayaTab.swift
//  Test
//
//  Created by Soumya Roychoudhury on 6/26/21.
//

import SwiftUI
import Foundation
import AVKit



struct Kramalaya: View {
    
    
    @StateObject var modifiers = ScreenModifiers()

    
    var videoIDs = ["W-TlsuIi9Ww", "4lG6EZ662kU", "aP1AhjWTUkg", "cPue-VR1ie0", "c-ZCCvlituA", "is7L0HFJKdY", "txrT_CAjakc", "2y4n6QcklQo", ]
    
    var videoNames = ["Tabla: 1, 2, 4, Tehai", "Recitation: 3 goon", "Tabla: 1 to 4, 8, Tehai", "Recitation: 5 goon", "Recitation: 6 goon","Recitation: 7 goon", "Recitation: 1 to 8, Tehai", "Tabla: 1 to 8, Tehai"]
    
      
    var body: some View
    {
        
        ZStack
        {
            LinearGradient(gradient: Gradient(colors: [Color.blue1, Color.grey2, Color.grey2]), startPoint: .top, endPoint: .bottom).ignoresSafeArea()
            
            
            VStack
            {
                Spacer()
                    .frame(height: modifiers.wt * 30)
                Image("TG1")
                    .resizable()
                    .frame(width: modifiers.ht * 300, height: modifiers.ht * 180)
                    .border(Color.black, width: 5)
                
                Spacer()
                    .frame(height: modifiers.ht * 30)
                
                Text("Kramalaya")
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
                    //Background - Clear to grey
                    HStack
                    {
                        Spacer()
                            .frame(height: modifiers.ht * 360)
                            .background(LinearGradient(gradient: Gradient(colors: [Color.clear, Color.clear, Color.clear, Color.clear, Color.grey2]), startPoint: .top, endPoint: .bottom))
                    }
                    
                    VStack(spacing:0)
                    {
                        
                        Group{
                            
                            HStack
                            {
                                Text("Kramalaya for Beginners")
                                    .fontWeight(.bold)
                                    .font(.system(size: modifiers.wt * 25))
                                
                                Spacer()
                                
                            }
                            .padding(10)
                            .background(Color.grey2)
                            .foregroundColor(.gold2)
                            .border(width: modifiers.wt * 2, edges: [.bottom], color: .gold2)
                            .padding(modifiers.wt * 20)
                            .background(Color.grey2)
                            
                            
                            ForEach(0..<1){video in
                                VStack{
                                    VideoList(videoID: videoIDs[video], videoName: videoNames[video])}
                            }
                        }
                        
                        
                        
                        Group{
                            
                            HStack
                            {
                                Text("Kramalaya for Intermediate")
                                    .fontWeight(.bold)
                                    .font(.system(size: modifiers.wt * 25))
                                
                                Spacer()
                                
                            }
                            .padding(10)
                            .background(Color.grey2)
                            .foregroundColor(.gold2)
                            .border(width: modifiers.wt * 2, edges: [.bottom], color: .gold2)
                            .padding(modifiers.wt * 20)
                            .background(Color.grey2)
                            
                            
                            ForEach(0..<2){video in
                                VStack{
                                    VideoList(videoID: videoIDs[video + 1], videoName: videoNames[video + 1])}
                            }
                        }

                        
                        Group{
                            
                            HStack
                            {
                                Text("Kramalaya for Advanced")
                                    .fontWeight(.bold)
                                    .font(.system(size: modifiers.wt * 25))
                                
                                Spacer()
                                
                            }
                            .padding(10)
                            .background(Color.grey2)
                            .foregroundColor(.gold2)
                            .border(width: modifiers.wt * 2, edges: [.bottom], color: .gold2)
                            .padding(modifiers.wt * 20)
                            .background(Color.grey2)
                            
                            
                            ForEach(0..<5){video in
                                VStack{
                                    VideoList(videoID: videoIDs[video + 3], videoName: videoNames[video + 3])}
                            }
                        }
                        
                        
                        
                        
                        
                    }.background(Color.grey2)

                }
            }
        }
    }
}



struct KramalayaTab_Previews: PreviewProvider {
    
    static var previews: some View {
        
        Group {
            
            Kramalaya().preferredColorScheme(/*@START_MENU_TOKEN@*/.dark/*@END_MENU_TOKEN@*/)
            
        }
        
    }
    
}

