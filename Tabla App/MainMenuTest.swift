//
//  MainMenuTest.swift
//  Tabla App
//
//  Created by Swagnik Roychoudhury on 4/6/22.
//

import SwiftUI
import AVKit
import Foundation


struct menuTest: View {
    
    let colors: [Color] = [.white, .gold2]
    
    
    var body: some View {
        GeometryReader { fullView in
            
            HStack{
                Spacer()
                    .frame(width: 50)
                
                ScrollView{
                    ForEach(0..<50) { index in
                        GeometryReader { geo in
                            Text("Panel #\(index + 1)")
                                .font(.title)
                                .frame(width: geo.size.width * 0.9, height: 400)
                                .background(colors[index%2])
                                .foregroundColor(colors[(index+1)%2])
                                .rotation3DEffect(.degrees(Double(geo.frame(in: .global).minY - (fullView.size.height/2)) / 20), axis: (x: 0, y: 0, z: 0))
                        }
                        .frame(height: 450, alignment: .center)
                    }
                }
                
                Spacer()
            }
            
        }
    }
}



struct menuTest_Previews: PreviewProvider {
    static var previews: some View {
        menuTest().preferredColorScheme(.dark)
    }
}
