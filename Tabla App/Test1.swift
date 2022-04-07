//
//  Test1.swift
//  Tabla App
//
//  Created by Debjani Roychoudhury on 4/6/22.
//


import SwiftUI

struct TestView: View {
    
    
    private func  getScale(proxy: GeometryProxy) -> CGFloat {
        var scale: CGFloat = 1
        let x = proxy.frame(in: .global).minX
        let diff = abs(x - 90)
        if diff < 200 {
            scale = 1 + (200 - diff) / 500
        }
        
        if scale == 1.4 {
            let impactMed = UIImpactFeedbackGenerator(style: .medium)
                impactMed.impactOccurred()
        }
        
        return  scale
    }
    
    
    var body: some View  {
        ScrollView(.horizontal) {
            HStack(spacing: 60) {
                Text("")
                    .frame(width: 0)
            
                ForEach(0..<10) { num in
                    GeometryReader { proxy in
                        
                        
                        VStack{
                            
                            let scale = getScale(proxy: proxy)
                            
                            
                            Image("Ghungroo4")
                                .resizable()
                                .scaledToFit()
                                .frame(width: 220)
                                .cornerRadius(30.0)
                                .clipped()
                                .shadow(color: .gold2, radius: 15, x: 0, y: 00)
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
    

struct Test_Previews: PreviewProvider {
    static var previews: some View {
        TestView()
    }
}
