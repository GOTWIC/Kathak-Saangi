//
//  Tabla.swift
//  Tabla App
//
//  Created by Swagnik Roychoudhury on 4/4/22.
//

import SwiftUI

import AVFoundation




struct Tabla: View {
    
    @StateObject var modifiers = ScreenModifiers()
    
    
    @State var audioPlayer: AVAudioPlayer?
    
    var trackNames = ["TablaSound5", "TablaSound3", "TablaSound4", "TablaSound7", "TablaSound6", "TablaSound8"]
    
    @State private var scaleValue1 = CGFloat(0.9)
    
    @State private var scaleValue2 = CGFloat(0.9)
    
    
    var body: some View {
        
    
        
        ZStack {
            
            
    
            Color.black
                .ignoresSafeArea()
            
            
            Image("Ghungroo4").centerCropped().ignoresSafeArea()

            VStack{

                Spacer()
                    .frame(height: modifiers.ht * 34)
                
                Text("Pressing different parts of the Tabla below will produce the different types of sounds that a real Tabla creates.")
                    .font(.custom("BodoniSvtyTwoOSITCTT-Bold", size: modifiers.wt * 12))
                    .multilineTextAlignment(.center)
                    .padding()
                    .frame(width: 300 * modifiers.wt)
                    .background(Color.gold2)
                    .overlay(
                        RoundedRectangle(cornerRadius: 10 * modifiers.ht)
                            .stroke(Color.white, lineWidth: 7 * modifiers.ht)
                    )
                    .cornerRadius(10.0 * modifiers.ht)
                    
                
                Spacer()
                
                ZStack{
   
                    VStack{
                        Image("TablaBackgroundImage")
                            .resizable()
                            .frame(width: modifiers.ht * 300, height: modifiers.ht * 300)
                            
                    }
                    
                    Circle()
                        .fill(Color.tablaColor1)
                        .frame(width: modifiers.ht * 238)
                        .onTapGesture {
                            playSound(track: trackNames[0], drumNum: 1)
                            let impactMed = UIImpactFeedbackGenerator(style: .light)
                            impactMed.impactOccurred()
                        }

                    Circle()
                        .fill(Color.tablaColor2)
                        .frame(width: modifiers.ht * 172)
                        .onTapGesture {
                            playSound(track: trackNames[1], drumNum: 1)
                            let impactMed = UIImpactFeedbackGenerator(style: .medium)
                            impactMed.impactOccurred()
                        }

                    Circle()
                        .fill(Color.tablaColor3)
                        .frame(width: modifiers.ht * 90)
                        .onTapGesture {
                            playSound(track: trackNames[2], drumNum: 1)
                            let impactMed = UIImpactFeedbackGenerator(style: .heavy)
                            impactMed.impactOccurred()
                        }

                    Circle()
                        .stroke(lineWidth: modifiers.ht * 7.0)
                        .fill(Color.tablaColor4)
                        .frame(width: modifiers.ht * 90)
                        .onTapGesture {
                            playSound(track: trackNames[2], drumNum: 1)
                            let impactMed = UIImpactFeedbackGenerator(style: .heavy)
                            impactMed.impactOccurred()
                        }

                }
                .scaleEffect(self.scaleValue1)


                ZStack{
                    
                    
                    VStack{
                        Image("TablaBackgroundImage")
                            .resizable()
                            .frame(width: modifiers.ht * 370, height: modifiers.ht * 370)
                            
                    }

                    Circle()
                        .fill(Color.tablaColor1)
                        .frame(width: modifiers.ht * 294)
                        .onTapGesture {
                            playSound(track: trackNames[3], drumNum: 2)
                            let impactMed = UIImpactFeedbackGenerator(style: .light)
                            impactMed.impactOccurred()
                        }

                    VStack{
                        
                        
                        ZStack{
                            
                            Circle()
                                .fill(Color.tablaColor2)
                                .frame(width: modifiers.ht * 205)
                                .onTapGesture {
                                    playSound(track: trackNames[4], drumNum: 2)
                                    let impactMed = UIImpactFeedbackGenerator(style: .medium)
                                    impactMed.impactOccurred()
                                }
                            
                            
                            VStack{
                                
                                ZStack{
                                    Circle()
                                        .fill(Color.tablaColor3)
                                        .frame(width: modifiers.ht * 105)
                                        .onTapGesture {
                                            playSound(track: trackNames[5], drumNum: 2)
                                            let impactMed = UIImpactFeedbackGenerator(style: .heavy)
                                            impactMed.impactOccurred()
                                        }

                                    Circle()
                                        .stroke(lineWidth: modifiers.ht * 7.0)
                                        .fill(Color.tablaColor4)
                                        .frame(width: modifiers.ht * 105)
                                        .onTapGesture {
                                            playSound(track: trackNames[5], drumNum: 2)
                                            let impactMed = UIImpactFeedbackGenerator(style: .heavy)
                                            impactMed.impactOccurred()
                                        }
                                }
                                
                                Spacer()
                                    .frame(height: modifiers.ht * 38)
                                
                            }
                        }
                        
                        Spacer()
                            .frame(height: modifiers.ht * 52)
                    }
                }
                .scaleEffect(self.scaleValue2)
                
                Spacer()
                        .frame(height: modifiers.ht * 20)

            }
        }
    }
    
    
    func playSound(track: String, drumNum: Int){
        
        withAnimation(.easeInOut(duration: 0.05)) {
            if(drumNum == 1){
                scaleValue1 = 0.8
            }
            else if(drumNum == 2){
                scaleValue2 = 0.8
            }
        }
        withAnimation(.easeInOut(duration: 0.05).delay(0.01)){
            scaleValue1 = 0.9
            scaleValue2 = 0.9
        }
        
        self.audioPlayer = try! AVAudioPlayer(contentsOf: URL(fileURLWithPath: Bundle.main.path(forResource: track, ofType: "wav")!))
        audioPlayer?.prepareToPlay()
        audioPlayer?.play()
    }
}


struct Tabla_Previews: PreviewProvider {
    static var previews: some View {
        Tabla()
    }
}
