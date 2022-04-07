//
//  SwiftUIView.swift
//  Tabla App
//
//  Created by Swagnik Roychoudhury on 9/3/21.
//

import SwiftUI
import AVKit

struct Lehra: View {
    
    @State var audioPlayer1: AVAudioPlayer?
    @State var audioPlayer2: AVAudioPlayer?
    @State var audioPlayer3: AVAudioPlayer?
    
    @State var isPlaying1 : Bool = false
    @State var isPlaying2 : Bool = false
    @State var isPlaying3 : Bool = false
    
    
    @State private var speed0: Double = 50
    @State private var speed1: Double = 50
    @State private var speed2: Double = 110
    @State private var speed3: Double = 170
    
    @StateObject var modifiers = ScreenModifiers()

        var body: some View {
            
            
            ZStack
            {
                
                Image("Ghungroo3").centerCropped()
                
                
                VStack {
                    
                    Group{
                        Spacer()
                        Spacer()
                        ZStack{
                            
                            Image("Border 2")
                                .resizable()
                                .frame(width: modifiers.ht * 300, height: modifiers.ht * 200)
                            
                            Text("\(speed0, specifier: "%.0f") BPM")
                                .foregroundColor(.gold2)
                                .font(.system(size: modifiers.wt * 50, weight: .bold))
                        }
                        Spacer()
                    }
                    
                    VStack{
                        Spacer()
                            .frame(height: modifiers.ht * 80)
                        
                        Group{
                            
                            VStack{
                                
                                HStack{
                                    
                                    Spacer()
                                    
                                    Text("50")
                                        .foregroundColor(.gold2)
                                        .font(.system(size: modifiers.ht * 24, weight: .bold))
                                    
                                    
                                    Spacer()
                                    
                                    Slider(value: Binding(
                                        get: {self.speed1},
                                        set: {(newSpeed) in
                                            if(isPlaying2 == false && isPlaying3 == false){
                                                self.speed0 = newSpeed}
                                            self.speed1 = newSpeed
                                            self.updateSpeed(player: audioPlayer1, baseSpeed: 65, currentSpeed: speed1)}
                                    ), in: 50...109, step: 1).frame(width: 234 * modifiers.wt, height: modifiers.ht * 20, alignment: /*@START_MENU_TOKEN@*/.center/*@END_MENU_TOKEN@*/)
                                    
                                    Spacer()
                                    
                                    Text("109")
                                        .foregroundColor(.gold2)
                                        .font(.system(size: modifiers.ht * 24, weight: .bold))
                                    
                                    Spacer()
                                }
                                .padding(.leading, modifiers.ht * 3)
                                .padding(.trailing, modifiers.ht * 3)
                                
                                
                                Button(action: {
                                    
                                    if(isPlaying1 == false)
                                    {
                                        stopAll()
                                        self.isPlaying1.toggle()
                                        updateSpeed(player: audioPlayer1, baseSpeed: 65, currentSpeed: speed1)
                                        audioPlayer1?.play()
                                        if(speed0 < 65 || speed0 > 99){
                                            speed0 = speed1
                                        }
                                    }
                                    
                                    else{
                                        self.isPlaying1.toggle()
                                        audioPlayer1?.stop()
                                    }
                                    
                                }) {
                                    Image(systemName: self.isPlaying1 == true ? "pause.fill" : "play")
                                        .resizable()
                                        .frame(width: modifiers.wt * 16, height: modifiers.wt * 19)
                                        .foregroundColor(.gold2)
                                }
                            }
                            .frame(width: (9 * UIScreen.main.bounds.size.width) / 10, height: 100, alignment: .center)
                        }
                        
                        Spacer()
                            .frame(height: modifiers.ht * 60)
                        
                        Group{
                            
                            HStack{
                                
                                Spacer()
                                
                                Text("110")
                                    .foregroundColor(.gold2)
                                    .font(.system(size: modifiers.ht * 24, weight: .bold))
                                
                                Spacer()
                                
                                Slider(value: Binding(
                                    get: {self.speed2},
                                    set: {(newSpeed) in
                                        if(isPlaying1 == false && isPlaying3 == false){
                                            self.speed0 = newSpeed}
                                        self.speed2 = newSpeed
                                        self.updateSpeed(player: audioPlayer2, baseSpeed: 120, currentSpeed: speed2)}
                                ), in: 110...169, step: 1).frame(width: 234 * modifiers.wt, height: modifiers.ht * 20, alignment: /*@START_MENU_TOKEN@*/.center/*@END_MENU_TOKEN@*/)
                                
                                Spacer()
                                
                                Text("169")
                                    .foregroundColor(.gold2)
                                    .font(.system(size: modifiers.ht * 24, weight: .bold))
                                
                                Spacer()
                            }
                            .padding(.leading, modifiers.ht * 3)
                            .padding(.trailing, modifiers.ht * 3)
                            
                            
                            Button(action: {
                                
                                if(isPlaying2 == false)
                                {
                                    stopAll()
                                    self.isPlaying2.toggle()
                                    updateSpeed(player: audioPlayer2, baseSpeed: 120, currentSpeed: speed2)
                                    audioPlayer2?.play()
                                    if(speed0 < 100 || speed0 > 169){
                                        speed0 = speed2
                                    }
                                }
                                
                                else{
                                    self.isPlaying2.toggle()
                                    audioPlayer2?.stop()
                                }
                                
                            }) {
                                Image(systemName: self.isPlaying2 == true ? "pause.fill" : "play")
                                    .resizable()
                                    .frame(width: modifiers.wt * 16, height: modifiers.wt * 19)
                                    .foregroundColor(.gold2)
                            }
                        }
                        
                        Spacer()
                            .frame(height: modifiers.ht * 60)
                        
                        Group{
                            
                            HStack{
                                
                                Spacer()
                                
                                Text("170")
                                    .foregroundColor(.gold2)
                                    .font(.system(size: modifiers.ht * 24, weight: .bold))
                                
                                Spacer()
                                
                                Slider(value: Binding(
                                    get: {self.speed3},
                                    set: {(newSpeed) in
                                        if(isPlaying1 == false && isPlaying2 == false){
                                            self.speed0 = newSpeed}
                                        self.speed3 = newSpeed
                                        self.updateSpeed(player: audioPlayer3, baseSpeed: 176, currentSpeed: speed3)}
                                ), in: 170...240, step: 1).frame(width: 234 * modifiers.wt, height: modifiers.ht * 20, alignment: /*@START_MENU_TOKEN@*/.center/*@END_MENU_TOKEN@*/)
                                
                                Spacer()
                                
                                Text("240")
                                    .foregroundColor(.gold2)
                                    .font(.system(size: modifiers.ht * 24, weight: .bold))
                                
                                Spacer()
                            }
                            .padding(.leading, modifiers.ht * 3)
                            .padding(.trailing, modifiers.ht * 3)
                            
                            
                            Button(action: {
                                
                                if(isPlaying3 == false)
                                {
                                    stopAll()
                                    self.isPlaying3.toggle()
                                    updateSpeed(player: audioPlayer3, baseSpeed: 176, currentSpeed: speed3)
                                    audioPlayer3?.play()
                                    if(speed0 < 170 || speed0 > 240){
                                        speed0 = speed3
                                    }
                                }
                                
                                else{
                                    self.isPlaying3.toggle()
                                    audioPlayer3?.stop()
                                }
                                
                            }) {
                                Image(systemName: self.isPlaying3 == true ? "pause.fill" : "play")
                                    .resizable()
                                    .frame(width: modifiers.wt * 16, height: modifiers.wt * 19)
                                    .foregroundColor(.gold2)
                            }
                        }
                        
                        Group{
                            Spacer()
                                .frame(height: modifiers.ht * 50)
                        }
                    }
                    .frame(width: modifiers.wt * 360, height: modifiers.ht * 420, alignment: .center)
                    .background(Color.black1)
                    .cornerRadius(modifiers.wt * 30.0)
                    
                    
                    Spacer()
                        .frame(height: modifiers.ht * 30)
                    
                }
                .onAppear(){
                    
                    
                    
                    self.audioPlayer1 = try! AVAudioPlayer(contentsOf: URL(fileURLWithPath: Bundle.main.path(forResource: "65 BPM Lehra Single", ofType: "mp3")!))
                    audioPlayer1?.enableRate = true
                    audioPlayer1?.numberOfLoops = -1
                    audioPlayer1?.prepareToPlay()
                    
                    self.audioPlayer2 = try! AVAudioPlayer(contentsOf: URL(fileURLWithPath: Bundle.main.path(forResource: "120 BPM Lehra Single", ofType: "mp3")!))
                    audioPlayer2?.enableRate = true
                    audioPlayer2?.numberOfLoops = -1
                    audioPlayer2?.prepareToPlay()
                    
                    self.audioPlayer3 = try! AVAudioPlayer(contentsOf: URL(fileURLWithPath: Bundle.main.path(forResource: "176 BPM Lehra Single", ofType: "mp3")!))
                    audioPlayer3?.enableRate = true
                    audioPlayer3?.numberOfLoops = -1
                    audioPlayer3?.prepareToPlay()
                    
                    
                    UIApplication.shared.isIdleTimerDisabled = true
                    
                    
                }
                .onDisappear(){
                    UIApplication.shared.isIdleTimerDisabled = false
                }
                .accentColor(.gold2)
                
            }
            
        }
    
    func updateSpeed(player: AVAudioPlayer?, baseSpeed: Double, currentSpeed: Double){
        player!.rate = Float(currentSpeed/baseSpeed)
    }
    
    func stopAll(){
        audioPlayer1?.stop()
        audioPlayer2?.stop()
        audioPlayer3?.stop()
        isPlaying1 = false
        isPlaying2 = false
        isPlaying3 = false
    }
    
}

struct Lehra_Previews: PreviewProvider {
    static var previews: some View {
        Lehra().preferredColorScheme(.dark)
    }
}
