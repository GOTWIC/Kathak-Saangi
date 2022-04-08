//
//  Test1.swift
//  Tabla App
//
//  Created by Debjani Roychoudhury on 4/6/22.
//


import SwiftUI

struct TestView: View {
    
    var tileTitles = ["Tabla Player", "Riyaz", "Kramalaya", "Ladi and Upaj", "Tehai", "Teentaal Lehra", "Octapad","Padhant", "Our Gurus", "Dasa Prana"]
    
    var tileDescriptions = ["Ever wanted to play the Tabla? Now you can, with this interactive Tabla Player!", "Use these practice audios for your daily riyaz of hand-movements, footwork, and circles.", "", "", "", "", "","", "", ""]
    
    
    @StateObject var modifiers = ScreenModifiers()
    
    
    
    private func  getScale(proxy: GeometryProxy) -> CGFloat {
        var scale: CGFloat = 1
        let x = proxy.frame(in: .global).minX
        let diff = abs(x - 90)
        if diff < 200 {
            scale = 1 + (200 - diff) / 500
        }
        
        if scale >= 1.392 {
            let impactMed = UIImpactFeedbackGenerator(style: .heavy)
                impactMed.impactOccurred()
        }
        
        return  scale
    }
    
    
    var body: some View  {
        
        ZStack{
            
            Image("StarPage")
                .resizable()
                .scaledToFill()
                .frame(width: modifiers.wt * 390 , alignment: .center)
            
//            Color.grey2
//                .ignoresSafeArea()
            
            ScrollView(.horizontal) {
                HStack(spacing: 60) {
                    Text("")
                        .frame(width: 0)
                
                    ForEach(0..<10) { num in
                        GeometryReader { proxy in
                            
                            VStack{
                                
                                let scale = getScale(proxy: proxy)

                                ZStack{
                                    
                                    
                                    Image("Ghungroo4")
                                        .resizable()
                                        .scaledToFit()
                                    
                                    
                                    VStack(alignment: .center){
                                        
                                        Spacer()
                                    
                                        
                                        Text("\(tileTitles[num])")
                                            .font(.system(size: 27, weight: .light, design: .serif))
                                        
                                        ZStack{
                                            Image("Tabla Image 1")
                                                .resizable()
                                                .scaledToFill()  
                                        }
                                        .frame(width: 190)
                                        
                                        
                                            .frame(width: modifiers.ht * 190, height: 100)
                                            .border(Color.black, width: 5)

                                        
                                        Spacer()
                                        
                                            
                                        
                                        Text("\(tileDescriptions[num])")
                                            .font(.system(size: 12, weight: .light, design: .serif))
                                            .multilineTextAlignment(.center)
                                            .padding()
                                            .frame(width: 190)
                                            .foregroundColor(.gold3)
                                            .background(Color.white1)
                                            .cornerRadius(20)
                                        
                                        
                                        Spacer()
                                            .frame(height: 30)
                                            
                                    }
                                    .frame(width: 220, height: 290)
                                    .background(Color.white2)
                                    .foregroundColor(Color.white)
                                    
        
                                }
                                .frame(width: 220)
                                .overlay(
                                            RoundedRectangle(cornerRadius: 30)
                                                .stroke(Color.white, lineWidth: 7)
                                        )
                                .cornerRadius(30.0)
                                .clipped()
                                .shadow(color: .red, radius: 30, x: 0, y: 15)
                                .rotation3DEffect(
                                    .degrees(-Double(proxy.frame(in: .global).minX - (90)) / 8),
                                    axis: (x: 0.0, y: 0.3, z: 0.0)
                                )
                                .scaleEffect(CGSize(width: scale, height: scale))
         
                                Spacer()
                                    .frame(height: 70)
                                
                                Text("Scaling \(scale)")
                                
                            }
                                
                        }
                        .frame(width: 200, height: 400)
                    }.frame(height: 600)
                    
                    Text("")
                        .frame(width: 10)
                }.padding(32)
            }
        }
        
        
    }
}
    

struct Test_Previews: PreviewProvider {
    static var previews: some View {
        TestView()
    }
}
