//
//  VideoLinkView.swift
//  Tabla App
//
//  Created by Soumya Roychoudhury on 8/29/21.
//

import SwiftUI
import Foundation
import AVKit


struct ItemList: View
 {
    var itemName: String
    
    @StateObject var modifiers = ScreenModifiers()
    
    
    var body: some View
    {
        HStack
        {
            
            Image(systemName: "arrow.right")
                .resizable()
                .frame(width: modifiers.wt * 14, height: modifiers.wt * 11)
                .foregroundColor(.gold2)
            Spacer()
                .frame(width: modifiers.wt * 15)
            
            Text("\(itemName)")
//                .fontWeight(.bold)
                .font(.system(size: modifiers.wt * 16))
            
            Spacer()
            
            Image(systemName: "ellipsis")
                .resizable()
                .frame(width: modifiers.wt * 15, height: modifiers.wt * 3)
                .foregroundColor(.gold2)
            
        }
        .padding(modifiers.wt * 10)
        .background(Color.grey2)
        .foregroundColor(.gold2)
        .border(width: modifiers.wt * 1, edges: [.bottom], color: .gold2)
        .padding(modifiers.wt * 20)
        .background(Color.grey2)
    }
}



