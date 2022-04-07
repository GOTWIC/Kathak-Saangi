//
//  VideoLinkView.swift
//  Tabla App
//
//  Created by Swagnik Roychoudhury on 8/29/21.
//

import SwiftUI
import Foundation
import AVKit
import WebKit


struct VideoList: View
 {
    
    @StateObject var modifiers = ScreenModifiers()
    
    @State var audioPlayer: AVAudioPlayer?
    
    @State var isPlaying : Bool = false
    
    //@Binding var trackName: String
    
    var videoID: String
    
    var videoName: String
    
    
    var body: some View
    {
        HStack
        {
            
            NavigationLink(destination:
            VideoView(videoID: videoID)
            ){
                
                Image(systemName: self.isPlaying == true ? "pause.fill" : "play")
                    .resizable()
                    .frame(width: modifiers.wt * 9, height: modifiers.wt * 11)
                    .foregroundColor(.gold2)
                Spacer()
                    .frame(width: modifiers.wt * 15)
                
                Text("\(videoName)")
                    .font(.system(size: modifiers.wt * 16))
                
                Spacer()
                
                Image(systemName: "ellipsis")
                    .resizable()
                    .frame(width: modifiers.wt * 15, height: modifiers.wt * 3)
                    .foregroundColor(.gold2)
            }
            
            
            
        }
        .padding(modifiers.wt * 10)
        .background(Color.grey2)
        .foregroundColor(.gold2)
        .border(width: modifiers.wt * 1, edges: [.bottom], color: .gold2)
        .padding(.horizontal, modifiers.wt * 20)
        .padding(.vertical, modifiers.wt * 10)
        .background(Color.grey2)

    }
}


struct VideoView: View
{
    @StateObject var modifiers = ScreenModifiers()
    
    var videoID: String
    
    var body: some View
    {
        ZStack{
            
            Image("StarPage").centerCropped()
            
            VideoFrame(videoID: videoID)
                .frame(maxWidth: UIScreen.main.bounds.size.width * 0.9, maxHeight: modifiers.ht * UIScreen.main.bounds.size.height * 0.3)
                .cornerRadius(modifiers.wt * 12)
                .background(Color.black)
                .foregroundColor(.black)
        }
    }
}


struct VideoFrame: UIViewRepresentable
{
    let videoID: String
    
    func makeUIView(context: Context) -> WKWebView {
        return WKWebView()
    }

    func updateUIView(_ uiView: WKWebView, context: Context) {
        
        
        guard let youtubeURL = URL(string: "https://www.youtube.com/embed/\(videoID)") else {return}
        uiView.scrollView.isScrollEnabled = false
        uiView.load(URLRequest(url: youtubeURL))
        uiView.backgroundColor = UIColor.black
            }
}

struct VideoList_Previews: PreviewProvider {
    
    static var previews: some View {
        
        Group {
            
            VideoList(videoID: "rCCc6Xt78pE", videoName: "Test").preferredColorScheme(/*@START_MENU_TOKEN@*/.dark/*@END_MENU_TOKEN@*/)
            
        }
        
    }
    
}
