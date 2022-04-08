//
//  Test3.swift
//  Tabla App
//
//  Created by Debjani Roychoudhury on 4/8/22.
//

import SwiftUI


private struct OffsetPreferenceKey: PreferenceKey {
    
    static var defaultValue: CGPoint = .zero
    
    static func reduce(value: inout CGPoint, nextValue: () -> CGPoint) { }
}

struct OffsettableScrollView<T: View>: View {
    let axes: Axis.Set
    let showsIndicator: Bool
    let onOffsetChanged: (CGPoint) -> Void
    let content: T
    
    init(axes: Axis.Set = .horizontal,
         showsIndicator: Bool = true,
         onOffsetChanged: @escaping (CGPoint) -> Void = { _ in },
         @ViewBuilder content: () -> T
    ) {
        self.axes = axes
        self.showsIndicator = showsIndicator
        self.onOffsetChanged = onOffsetChanged
        self.content = content()
    }
    
    var body: some View {
        ScrollView(axes, showsIndicators: showsIndicator) {
            GeometryReader { proxy in
                Color.clear.preference(
                    key: OffsetPreferenceKey.self,
                    value: proxy.frame(
                        in: .named("ScrollViewOrigin")
                    ).origin
                )
            }
            .frame(width: 0, height: 0)
            content
        }
        .coordinateSpace(name: "ScrollViewOrigin")
        .onPreferenceChange(OffsetPreferenceKey.self,
                            perform: onOffsetChanged)
    }
}

//struct Test3: View {
//
//
//    var tileTitles = ["Tabla Player", "Riyaz", "Kramalaya", "Ladi and Upaj", "Tehai", "Teentaal Lehra", "Octapad","Padhant", "Our Gurus", "Dasa Prana"]
//
//    var tileDescriptions = ["Ever wanted to play the Tabla? Now you can, with this interactive Tabla Player!", "Use these practice audios for your daily riyaz of hand-movements, footwork, and circles", "Practice audio for Kramalaya (Chromatic Speed), ranging from Beginner to Advanced", "Practice Laris, a composition created by different variations of a theme, and its improv counterpart, Upaj", "Explore the never-seen-before mathematics behind different types of tehais, through tutorials and calculators", "A simple Teentaal Lehra with adjustable speeds", "Western compositions created on the Octapad, for the Kathakar's own choreography","Learn complex and tongue-twisting compositions to improve your recitation skills, including a phrase-by-phrase breakdown", "Specially curated tutorials and showcases by Guru Sandip Mallick and Guru Aniruddha Mukherjee", "An article detailing the Dasa Prana, the 10 Vital Elements of Taal"]
//
//    var tileImages = ["Ghungroo1", "Tabla Image 1", "TG1", "GhungrooEdited2", "Tabla Image 3", "Ghungroo3", "Octapad", "Padhant", "Ghungroo1", "Tabla Image 1"]
//
//
//
//    @StateObject var modifiers = ScreenModifiers()
//
//    @State private var currentIndex = 0
//
//
//
//    private func  getScale(proxy: GeometryProxy) -> CGFloat {
//        var scale: CGFloat = 1
//        let x = proxy.frame(in: .global).minX
//        let diff = abs(x - 90)
//        if diff < 200 {
//            scale = 1 + (200 - diff) / 500
//        }
//
//        if scale >= 1.392 {
//            let impactMed = UIImpactFeedbackGenerator(style: .heavy)
//                impactMed.impactOccurred()
//        }
//
//        return  scale
//    }
//
//    @State private var horizontalOffset: CGFloat = 0.0
//
//
//
//    var body: some View {
//
//        VStack {
//            Text("Offset: \(String(format: "%.2f", horizontalOffset))")
//                .frame(maxWidth: .infinity)
//                .padding()
//                .background(Color.yellow)
//
//            OffsettableScrollView { point in
//                horizontalOffset = point.x
//            } content: {
//                LazyHStack {
//
//                    Spacer()
//                        .frame(width: 50)
//
//                    ForEach(0..<10) { num in
//
//                        Spacer()
//                            .frame(width: 30)
//
//                        GeometryReader { proxy in
//
//
//                            NavigationLink(destination: Riyaz()){
//
//                                VStack{
//
//                                    let scale = getScale(proxy: proxy)
//
//                                    ZStack{
//
//                                        Color.grey2
//
//
//    //                                    Image("Ghungroo3")
//    //                                        .resizable()
//    //                                        .scaledToFit()
//
//
//
//                                        VStack(alignment: .center){
//
//                                            Spacer()
//                                                .frame(height: 15)
//
//
//                                            ZStack{
//                                                Image(tileImages[num])
//                                                    .resizable()
//                                                    .scaledToFill()
//                                                    .scaleEffect(1.2)
//
//                                                Text("")
//                                                    .frame(width:190, height: 120)
//                                                    .background(Color.white3)
//
//
//                                                Text("\(tileTitles[num])")
//                                                    .font(.system(size: 27, weight: .bold, design: .serif))
//                                                    .multilineTextAlignment(.center)
//                                                    .padding(10)
//                                                    .foregroundColor(.black)
//    //                                                .background(Color.white1)
//    //                                                .cornerRadius(10)
//
//
//                                            }
//                                            .frame(width: 190, height: 120)
//                                            .clipped()
//                                            .cornerRadius(20)
//
//
//
//                                            Spacer()
//
//                                            Text("\(tileDescriptions[num])")
//                                                .font(.system(size: 12, weight: .light, design: .serif))
//                                                .multilineTextAlignment(.center)
//                                                .padding()
//                                                .frame(width: 190)
//                                                .foregroundColor(.gold3)
//                                                .background(Color.white1)
//                                                .cornerRadius(20)
//
//
//                                            Spacer()
//                                                .frame(height: 20)
//
//                                        }
//                                        .frame(width: 220, height: 290)
//                                        //.background(Color.white2)
//                                        .foregroundColor(Color.white)
//
//
//                                    }
//                                    .frame(width: 220)
//                                    .overlay(
//                                                RoundedRectangle(cornerRadius: 30)
//                                                    .stroke(Color.white, lineWidth: 7)
//                                            )
//                                    .cornerRadius(30.0)
//                                    .clipped()
//                                    .shadow(color: .orange, radius: 20, x: 0, y: 10)
//                                    .rotation3DEffect(
//                                        .degrees(-Double(proxy.frame(in: .global).minX - (90)) / 8),
//                                        axis: (x: 0, y: 0.3, z: 0)
//                                    )
//                                    .scaleEffect(CGSize(width: scale, height: scale))
//
//                                    Spacer()
//                                        .frame(height: 70)
//
//                                    //Text("Scaling \(scale)")
//
//                                }
//                            }
//
//
//                        }
//                        .frame(width: 200, height: 400)
//                        .id(num)
//
//                        Spacer()
//                            .frame(width: 30)
//
//                    }
//
//                    Spacer()
//                        .frame(width: 70)
//                }
//            }
//        }
//    }
//}

struct Test3_Previews: PreviewProvider {
    static var previews: some View {
        Test3()
    }
}

struct Test3: View {
    @State private var offset = CGFloat.zero
    var body: some View {
        ScrollView {
            VStack {
                ForEach(0..<100) { i in
                    Text("Item \(i)").padding()
                }
            }.background(GeometryReader {
                Color.clear.preference(key: ViewOffsetKey.self,
                    value: -$0.frame(in: .named("scroll")).origin.y)
            })
            .onPreferenceChange(ViewOffsetKey.self) { print("offset >> \($0)") }
        }.coordinateSpace(name: "scroll")
    }
}

struct ViewOffsetKey: PreferenceKey {
    typealias Value = CGFloat
    static var defaultValue = CGFloat.zero
    static func reduce(value: inout Value, nextValue: () -> Value) {
        value += nextValue()
    }
}
