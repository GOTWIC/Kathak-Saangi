//
//  AudioList.swift
//  Test
//
//  Created by Soumya Roychoudhury on 6/19/21.
//

import SwiftUI
import Foundation
import AVKit



struct AudioList: View
 {
    
    @State var audioPlayer: AVAudioPlayer?
    
    @State var isPlaying : Bool = false
    
    
    var trackName: String
    
    var body: some View
    {
        HStack
        {
            //Image(systemName: "play")
            Button(action: {
                self.isPlaying.toggle()
                    if(isPlaying == true)
                    {
                        audioPlayer?.play()
                        //audioPlayer?.numberOfLoops = -1
                        
                        
                    }
                    
                    else{
                        audioPlayer?.stop()
                    }
            }) {
                Image(systemName: self.isPlaying == true ? "pause.fill" : "play")

                    .foregroundColor(.gold2)
            }
            
            Text("\(trackName)")
                .fontWeight(.bold)
            
            Spacer()
            
            
            Image(systemName: "ellipsis")
        }
        .padding(10)
        .background(Color.grey2)
        .foregroundColor(.gold2)
        .border(width: 1, edges: [.bottom], color: .gold2)
        .padding(20)
        .background(Color.grey2)
        .onAppear {
            

            self.audioPlayer = try! AVAudioPlayer(contentsOf: URL(fileURLWithPath: Bundle.main.path(forResource: trackName, ofType: "mp3")!))
            
            
            audioPlayer?.enableRate = true
            audioPlayer?.prepareToPlay()
            audioPlayer?.rate = 1
            
            
        }
    }
    
    
}



struct AudioList_Previews: PreviewProvider {
    
    
    
    static var previews: some View {
        Group {
            
            AudioList(trackName: "120 BPM Single")
            
        }
        
    }
    
}

