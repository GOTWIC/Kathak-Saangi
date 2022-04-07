//
//  Ladi.swift
//  Tabla App
//
//  Created by Swagnik Roychoudhury on 9/5/21.
//

import SwiftUI
import AVKit
import Foundation


struct Ladi_Upaj: View {
    
    var videoID01 = ""
    var videoID02 = "s3ey05kEUpw"
    var videoName01 = "Example Ladi 1"
    var videoName02 = "Example Ladi 2"
    
    var videoIDs = ["gvRYnu5bId0", "MugqAGqO24I", "s3ey05kEUpw", "cWv5eGRiFuA", "rnmFQH5ZTzo", "XGo2YVYQbZI"]
    
    var videoNames = ["Example Ladi 1: Tabla", "Example Ladi 1: Recitation", "Example Ladi 2: Tabla", "Example Ladi 2: Recitation", "Example Upaj 1", "Example Upaj 2"]
    
    @StateObject var modifiers = ScreenModifiers()
    
    var body: some View
    {
        //SampleVideoPlayer2()
        
        //NavigationView{
        
        ZStack
        {
            LinearGradient(gradient: Gradient(colors: [Color.gold2, Color.grey2, Color.grey2]), startPoint: .top, endPoint: .bottom).ignoresSafeArea()
            VStack
            {
                Spacer()
                    .frame(height: modifiers.ht * 50)
                Image("GhungrooEdited2")
                    .resizable()
                    .frame(width: modifiers.ht * 300, height: modifiers.ht * 187)
                    .border(Color.black, width: 5)
                
                Spacer()
                    .frame(height: modifiers.wt * 30)
                
                Text("Ladi and Upaj")
                    .foregroundColor(.gold2)
                    .font(.system(size: modifiers.wt * 24, weight: .bold))
                    .frame(width: modifiers.wt * 200, height: modifiers.ht * 25, alignment: .center)
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
                        Text("Ladi")
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
                        
                        Text("Ladi literally means a sequence of rhythmic variations. Beginning with a theme, different patterns of that theme proceed through associated avartans of taal and ladi finally culminates on tihai. The Ladi mainly focuses on different styles of footwork. Ladis are previously composed and choreographed rhythmic patterns.")
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
                    
                    ForEach(0..<4){video in
                        VStack{
                            VideoList(videoID: videoIDs[video], videoName: videoNames[video])
                        }
                    }
                    
                    HStack
                    {
                        Text("Upaj")
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
                        
                        Text("Upaj is an impromptu and spontaneous improvisation of layakari with rhythmic bols weaved in multiple avartan of taal. It is a rendition of different compositions by dancers and tabla players. They both comprehend layers of bols and layakari on the canvas of avartans of taal reflecting dynamic applications of micro mathematics. This is mainly footwork for Kathak dancers but can be accompanied by extemporaneous hand and body movements. The Upaj cannot be taught, as it is similar to a lari but is created on-the-spot. With the enough practice and experience, artists are able to wonderfully design the mnemonic syllables on the go, which culminates into Upaj. Because Upajs are not created beforehand, the bols are often simpler than that of the Lari.")
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
                    
                    ForEach(0..<2){video in
                        VStack{
                            VideoList(videoID: videoIDs[video + 4], videoName: videoNames[video + 4])
                        }
                    }
                    
                }
            }
        }
    }
}

struct Ladi_Previews: PreviewProvider {
    
    static var previews: some View {
        
        Group {
            
            Ladi_Upaj().preferredColorScheme(/*@START_MENU_TOKEN@*/.dark/*@END_MENU_TOKEN@*/)
            
        }
        
    }
    
}
