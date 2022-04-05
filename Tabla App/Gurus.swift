//
//  Gurus.swift
//  Tabla App
//
//  Created by Soumya Roychoudhury on 9/5/21.
//
import SwiftUI
import Foundation
import AVKit



struct Gurus: View {
    
    
    @StateObject var modifiers = ScreenModifiers()

    
    var videoIDs = ["A_hUoVJ9I24", "LA3h5bHnPA0", "cqB-IyvRvpw", "Yp5kbeWzQCs", "pEHmOCQIhZE", "e3ijdINwvZo", "S-2pp4eHD0M", "dsvFODVTdqs", "70nOS-t3yts",]
    
    var videoNames = ["2 Beat Hastak", "3 Beat Hastak", "4 Beat Hastak", "3 Beat Chakkar", "5 Beat Chakkar", "Geometrical Analysis of Kathak", "Variations of Kathak Footwork", "Introduction to Uthaan", "Example of Uthaan",]
    
      
    var body: some View
    {
        
        ZStack
        {
            LinearGradient(gradient: Gradient(colors: [Color.green2, Color.grey2, Color.grey2]), startPoint: .top, endPoint: .bottom).ignoresSafeArea()
            
            
            VStack
            {
                Spacer()
                    .frame(height: modifiers.wt * 30)
                
                HStack(alignment: .center, spacing: 0, content: {
                    
                    Image("sandip")
                        .resizable()
                        .frame(width: modifiers.ht * 160, height: modifiers.ht * 160)
                    
                    Image("Aniruddha")
                        .resizable()
                        .frame(width: modifiers.ht * 160, height: modifiers.ht * 160)
                    
                })
                .border(width: 5, edges: [.top, .bottom, .leading, .trailing], color: Color.black)
        
                
                
                Spacer()
                    .frame(height: modifiers.ht * 30)
                
                Text("Our Gurus")
                    .foregroundColor(.gold2)
                    .font(.system(size: modifiers.wt * 24, weight: .bold))
                    .frame(width: modifiers.wt * 160, height: modifiers.ht * 25, alignment: .center)
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
                                Text("Hastak Demonstration")
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
                            
                            
                            ForEach(0..<3){video in
                                VStack{
                                    VideoList(videoID: videoIDs[video], videoName: videoNames[video])}
                            }
                        }
                        
                        Group{
                            
                            HStack
                            {
                                Text("Chakkar Demonstration")
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
                                    VideoList(videoID: videoIDs[video + 3], videoName: videoNames[video + 3])}
                            }
                        }
                        
                        Group{
                            
                            HStack
                            {
                                Text("Guru Sandip Mallick")
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
                                    VideoList(videoID: videoIDs[video + 5], videoName: videoNames[video + 5])}
                            }
                        }
                        
                        Group{
                            
                            HStack
                            {
                                Text("Guru Aniruddha Mukherjee")
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
                                    VideoList(videoID: videoIDs[video + 7], videoName: videoNames[video + 7])}
                            }
                        }
                        
                        
                        
                        
                        
                    }.background(Color.grey2)

                }
            }
        }
    }
}



struct SandipMallick_Previews: PreviewProvider {
    
    static var previews: some View {
        
        Group {
            
            Gurus().preferredColorScheme(/*@START_MENU_TOKEN@*/.dark/*@END_MENU_TOKEN@*/)
            
        }
        
    }
    
}



