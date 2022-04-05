//
//  Color.swift
//  Tabla
//
//  Created by Soumya Roychoudhury on 3/14/21.
//

import SwiftUI
import UIKit

extension Color {
    
    public static let gold1: Color = Color(UIColor(red: 185/255, green: 150/255, blue: 100/255, alpha: 0.5))
    
    public static let gold2: Color = Color(UIColor(red: 185/255, green: 150/255, blue: 100/255, alpha: 1.0))
    
    public static let grey1: Color = Color(UIColor(red: 40/255, green: 40/255, blue: 40/255, alpha: 1.0))
    
    public static let grey2: Color = Color(UIColor(red: 35/255, green: 31/255, blue: 31/255, alpha: 1.0))
    
    public static let blue1: Color = Color(UIColor(red: 5/255, green: 70/255, blue: 130/255, alpha: 1.0))
    
    public static let blue2: Color = Color(UIColor(red: 0/255, green: 130/255, blue: 130/255, alpha: 1.0))
    
    public static let green1: Color = Color(UIColor(red: 10/255, green: 185/255, blue: 70/255, alpha: 1.0))
    
    public static let green2: Color = Color(UIColor(red: 145/255, green: 160/255, blue: 50/255, alpha: 1.0))
    
    public static let purple1: Color = Color(UIColor(red: 50/255, green: 0/255, blue: 85/255, alpha: 1.0))
    
    public static let red1: Color = Color(UIColor(red: 170/255, green: 0/255, blue: 25/255, alpha: 0.2))
    
    public static let red2: Color = Color(UIColor(red: 120/255, green: 0/255, blue: 25/255, alpha: 1.0))
    
    public static let red3: Color = Color(UIColor(red: 100/255, green: 0/255, blue: 55/255, alpha: 0.5))

    public static let black1: Color = Color(UIColor(red: 0/255, green: 0/255, blue: 0/255, alpha: 0.5))
    
    public static let black2: Color = Color(UIColor(red: 0/255, green: 0/255, blue: 0/255, alpha: 0.95))
    
    public static let black3: Color = Color(UIColor(red: 0/255, green: 0/255, blue: 0/255, alpha: 0.8))
    
    public static let orange1: Color = Color(UIColor(red: 140/255, green: 70/255, blue: 0/255, alpha: 0.7))
    
    public static let yellow1: Color = Color(UIColor(red: 215/255, green: 210/255, blue: 105/255, alpha: 0.7))
    
    
}

extension View {
          
    func border(width: CGFloat, edges: [Edge], color: Color) -> some View {overlay(EdgeBorder(width: width, edges: edges).foregroundColor(color))
        
    }
    
    func hideKeyboard() {
            UIApplication.shared.sendAction(#selector(UIResponder.resignFirstResponder), to: nil, from: nil, for: nil)
        }
    
    
    
}


struct EdgeBorder: Shape {

    var width: CGFloat
    var edges: [Edge]

    func path(in rect: CGRect) -> Path {
        var path = Path()
        for edge in edges {
            var x: CGFloat {
                switch edge {
                case .top, .bottom, .leading: return rect.minX
                case .trailing: return rect.maxX - width
                }
            }

            var y: CGFloat {
                switch edge {
                case .top, .leading, .trailing: return rect.minY
                case .bottom: return rect.maxY - width
                }
            }

            var w: CGFloat {
                switch edge {
                case .top, .bottom: return rect.width
                case .leading, .trailing: return self.width
                }
            }

            var h: CGFloat {
                switch edge {
                case .top, .bottom: return self.width
                case .leading, .trailing: return rect.height
                }
            }
            path.addPath(Path(CGRect(x: x, y: y, width: w, height: h)))
        }
        return path
    }
}

class ScreenModifiers: ObservableObject {
    @Published var wt = UIScreen.main.bounds.size.width / 390
    @Published var ht = UIScreen.main.bounds.size.height / 844
}


struct ThemeAnimationStyle: ButtonStyle {
    
    @StateObject var modifiers = ScreenModifiers()
    
    var splash: Bool
    
    func makeBody(configuration: Self.Configuration) -> some View {
        
        if splash == false{
            configuration.label
                .frame(width: modifiers.wt * 360, height: modifiers.ht * 60, alignment: .center)
                .background(configuration.isPressed ? Color.gold2.opacity(0.1) : Color.clear)
                .cornerRadius(20)
                .scaleEffect(configuration.isPressed ? 0.8 : 1.0)
        }
        
        else{
            configuration.label
                .frame(width: modifiers.wt * 360, height: modifiers.ht * 60, alignment: .center)
                .cornerRadius(20)
                .scaleEffect(configuration.isPressed ? 0.8 : 1.0)
        }
    }
}

extension Image {
    func centerCropped() -> some View {
        GeometryReader { geo in
            self
            .resizable()
            .scaledToFill()
            .frame(width: geo.size.width, height: geo.size.height)
            .clipped()
        }
    }
}






