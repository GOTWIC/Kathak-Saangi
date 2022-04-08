import SwiftUI

struct ScrollingHStackModifier: ViewModifier {
    
    @State private var scrollOffset: CGFloat
    @State private var dragOffset: CGFloat
    
    var items: Int
    var itemWidth: CGFloat
    var itemSpacing: CGFloat
    
    init(items: Int, itemWidth: CGFloat, itemSpacing: CGFloat) {
        self.items = items
        self.itemWidth = itemWidth
        self.itemSpacing = itemSpacing
        
        // Calculate Total Content Width
        let contentWidth: CGFloat = CGFloat(items) * itemWidth + CGFloat(items - 1) * itemSpacing
        let screenWidth = UIScreen.main.bounds.width
        
        // Set Initial Offset to first Item
        let initialOffset = (contentWidth/2.0) - (screenWidth/2.0) + ((screenWidth - itemWidth) / 2.0)
        
        self._scrollOffset = State(initialValue: initialOffset)
        self._dragOffset = State(initialValue: 0)
    }
    
    func body(content: Content) -> some View {
        content
            .offset(x: scrollOffset + dragOffset, y: 0)
            .gesture(DragGesture()
                .onChanged({ event in
                    dragOffset = event.translation.width
                })
                .onEnded({ event in
                    // Scroll to where user dragged
                    scrollOffset += event.translation.width
                    dragOffset = 0
                    
                    // Now calculate which item to snap to
                    let contentWidth: CGFloat = CGFloat(items) * itemWidth + CGFloat(items - 1) * itemSpacing
                    let screenWidth = UIScreen.main.bounds.width
                    
                    // Center position of current offset
                    let center = scrollOffset + (screenWidth / 2.0) + (contentWidth / 2.0)
                    
                    // Calculate which item we are closest to using the defined size
                    var index = (center - (screenWidth / 2.0)) / (itemWidth + itemSpacing)
                    
                    // Should we stay at current index or are we closer to the next item...
                    if index.remainder(dividingBy: 1) > 0.5 {
                        index += 1
                    } else {
                        index = CGFloat(Int(index))
                    }
                    
                    // Protect from scrolling out of bounds
                    index = min(index, CGFloat(items) - 1)
                    index = max(index, 0)
                    
                    // Set final offset (snapping to item)
                    let newOffset = index * itemWidth + (index - 1) * itemSpacing - (contentWidth / 2.0) + (screenWidth / 2.0) - ((screenWidth - itemWidth) / 2.0) + itemSpacing
                    
                    // Animate snapping
                    withAnimation(Animation.easeIn(duration: 0.1)) {
                        scrollOffset = newOffset
                    }
                    
                })
            )
    }
}



struct ContentView2: View {
    
    var colors: [Color] = [.blue, .green, .red, .orange]
    
    
    var tileTitles = ["Tabla Player", "Riyaz", "Kramalaya", "Ladi and Upaj", "Tehai", "Teentaal Lehra", "Octapad","Padhant", "Our Gurus", "Dasa Prana"]
    
    var tileDescriptions = ["Ever wanted to play the Tabla? Now you can, with this interactive Tabla Player!", "Use these practice audios for your daily riyaz of hand-movements, footwork, and circles", "Practice audio for Kramalaya (Chromatic Speed), ranging from Beginner to Advanced", "Practice Laris, a composition created by different variations of a theme, and its improv counterpart, Upaj", "Explore the never-seen-before mathematics behind different types of tehais, through tutorials and calculators", "A simple Teentaal Lehra with adjustable speeds", "Western compositions created on the Octapad, for the Kathakar's own choreography","Learn complex and tongue-twisting compositions to improve your recitation skills, including a phrase-by-phrase breakdown", "Specially curated tutorials and showcases by Guru Sandip Mallick and Guru Aniruddha Mukherjee", "An article detailing the Dasa Prana, the 10 Vital Elements of Taal"]
    
    var tileImages = ["Ghungroo1", "Tabla Image 1", "TG1", "GhungrooEdited2", "Tabla Image 3", "Ghungroo3", "Octapad", "Padhant", "", ""]
    
    
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
    
    
    var body: some View {
        
        
        ZStack{
            
            
            Image("WP1").centerCropped()
                //.resizable()
                //.scaledToFill()
                //.frame(width: modifiers.wt * 390 , alignment: .center)
            
            
            HStack(alignment: .center, spacing: 60) {
                
    //            Text("")
    //                .frame(width: 0)
                
                ForEach(0..<10) { num in
                    GeometryReader { proxy in


                        Text("Hello there")
                            .frame(width:180, height: 300)
                            .background(Color.white)
                        
//                        NavigationLink(destination: Riyaz()){
//
//                            VStack{
//
//                                let scale = getScale(proxy: proxy)
//
//                                ZStack{
//
//                                    Color.grey2
//
//
//    //                                    Image("Ghungroo3")
//    //                                        .resizable()
//    //                                        .scaledToFit()
//
//
//
//                                    VStack(alignment: .center){
//
//                                        Spacer()
//                                            .frame(height: 15)
//
//
//                                        ZStack{
//                                            Image(tileImages[num])
//                                                .resizable()
//                                                .scaledToFill()
//                                                .scaleEffect(1.2)
//
//                                            Text("")
//                                                .frame(width:190, height: 120)
//                                                .background(Color.white3)
//
//
//                                            Text("\(tileTitles[num])")
//                                                .font(.system(size: 27, weight: .bold, design: .serif))
//                                                .multilineTextAlignment(.center)
//                                                .padding(10)
//                                                .foregroundColor(.black)
//    //                                                .background(Color.white1)
//    //                                                .cornerRadius(10)
//
//
//                                        }
//                                        .frame(width: 190, height: 120)
//                                        .clipped()
//                                        .cornerRadius(20)
//
//
//
//                                        Spacer()
//
//                                        Text("\(tileDescriptions[num])")
//                                            .font(.system(size: 12, weight: .light, design: .serif))
//                                            .multilineTextAlignment(.center)
//                                            .padding()
//                                            .frame(width: 190)
//                                            .foregroundColor(.gold3)
//                                            .background(Color.white1)
//                                            .cornerRadius(20)
//
//
//                                        Spacer()
//                                            .frame(height: 20)
//
//                                    }
//                                    .frame(width: 220, height: 290)
//                                    //.background(Color.white2)
//                                    .foregroundColor(Color.white)
//
//
//                                }
//                                .frame(width: 220)
//                                .overlay(
//                                            RoundedRectangle(cornerRadius: 30)
//                                                .stroke(Color.white, lineWidth: 7)
//                                        )
//                                .cornerRadius(30.0)
//                                .clipped()
////                                .shadow(color: .orange, radius: 20, x: 0, y: 10)
//                                .rotation3DEffect(
//                                    .degrees(-Double(proxy.frame(in: .global).minX - (90)) / 8),
//                                    axis: (x: 0, y: 0.3, z: 0)
//                                )
//                                .scaleEffect(CGSize(width: scale, height: scale))
//
//                                Spacer()
//                                    .frame(height: 70)
//
//                                //Text("Scaling \(scale)")
//
//                            }
//                        }


                    }
                    .frame(width: 200, height: 400)
                }.frame(height: 600)
                
    //            Text("")
    //                .frame(width: 0)
            }.modifier(ScrollingHStackModifier(items: tileTitles.count, itemWidth: 200, itemSpacing: 60))
            
            
        }
        
        
        
        
        
    }
}


struct Test2_Previews: PreviewProvider {
    static var previews: some View {
        ContentView2()
    }
}
