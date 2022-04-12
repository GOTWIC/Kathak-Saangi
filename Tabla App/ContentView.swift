//

//  ContentView.swift

//  Tabla

//

//  Created by Swagnik Roychoudhury on 3/14/21.

//



import SwiftUI
import AVKit
import Foundation


struct ContentView_Previews: PreviewProvider {
    
    static var previews: some View {
        
        Group {
            ContentView().preferredColorScheme(.dark)
            
            
        }
        
    }
    
}

struct ContentView: View {
    
    var body: some View {
       
        NavigationView{
            
            Splash()
            .navigationBarTitle("")
            .navigationBarHidden(true)
            
            //Home()
        }.navigationViewStyle(StackNavigationViewStyle())
        
    }
    
}

struct Splash: View {
    
    @StateObject var modifiers = ScreenModifiers()
    
    var body: some View{
        

        VStack{
        
            VStack{

                Spacer()

                Text("Kathak Saangi")
                    .font(.custom("BodoniSvtyTwoOSITCTT-Bold", size: modifiers.wt * 55))
                    .foregroundColor(.gold2)
                    .frame(width: modifiers.wt * 360, height: modifiers.ht * 100, alignment: .center)
                    //.background(Color.gold1)
                    .cornerRadius(modifiers.wt * 30.0)
                
                
                Text("The Ultimate")
                    .font(.custom("BodoniSvtyTwoOSITCTT-Bold", size: modifiers.ht * 30))
                    .foregroundColor(.gold2)
                    .frame(width: modifiers.wt * 360, height: modifiers.ht * 40, alignment: .center)
                    .fixedSize(horizontal: false, vertical: true)
                    .multilineTextAlignment(.center)
                    //.background(Color.gold1)
                    .cornerRadius(modifiers.wt * 30.0)
                
                Text("Kathakar's Companion")
                    .font(.custom("BodoniSvtyTwoOSITCTT-Bold", size: modifiers.ht * 30))
                    .foregroundColor(.gold2)
                    .frame(width: modifiers.wt * 360, height: modifiers.ht * 40, alignment: .center)
                    .fixedSize(horizontal: false, vertical: true)
                    .multilineTextAlignment(.center)
                    //.background(Color.gold1)
                    .cornerRadius(modifiers.wt * 30.0)


                Spacer()
                
                Image("Dancer")
                    .resizable()
                    //.aspectRatio(contentMode: .fill)
                    .frame(width: modifiers.wt * 300, height: modifiers.wt * 320, alignment: .center)
                    .background(Color.gold1)
                    .cornerRadius(modifiers.wt * 30.0)

                
                Spacer()

                NavigationLink(destination: Home().preferredColorScheme(.dark).onAppear(){let impactMed = UIImpactFeedbackGenerator(style: .light); impactMed.impactOccurred()}){

                    Text("Enter")
                        .font(.custom("BodoniSvtyTwoOSITCTT-Bold", size: modifiers.wt * 40))
                        .foregroundColor(.black)
                        .frame(width: modifiers.wt * 200, height: modifiers.ht * 70, alignment: .center)
                        .background(Color.gold1)
                        .cornerRadius(modifiers.wt * 30.0)

                }.buttonStyle(ThemeAnimationStyle(splash: true))


                Spacer()

            }
            
            
            Spacer()
        }.background(
            Image("TB2")
                .resizable()
                .opacity(1)
                .aspectRatio(contentMode: .fill)
            
            
        )
            

    }
}


struct Home: View {
    
    var test = [Riyaz(), TestView(), Riyaz(), TestView(), Riyaz(), TestView(), Riyaz(), TestView(), Riyaz(), TestView()] as [Any]
    
    @State var showInfo = false
    
    @State var progressValue: Float = 0.0
    
    @State var leftFlag: Bool = false
    
    @State var rightFlag: Bool = false
    
    @State private var currentIndex = 0
    
    @State private var horizontalOffset: CGFloat = 0.0
    
    @StateObject var modifiers = ScreenModifiers()
    
    var tileTitles = ["Tabla Player", "Riyaz", "Kramalaya", "Ladi and Upaj", "Tehai", "Teentaal Lehra", "Octapad","Padhant", "Our Gurus", "Dasa Prana"]
    
    var tileDescriptions = ["Ever wanted to play the Tabla? Now you can, with this interactive Tabla Player!", "Use these practice audios for your daily riyaz of hand-movements, footwork, and circles", "Practice audio for Kramalaya (Chromatic Speed), ranging from Beginner to Advanced", "Practice Laris, a composition created by different variations of a theme, and its improv counterpart, Upaj", "Explore the never-seen-before mathematics behind different types of tehais, through tutorials and calculators", "A simple Teentaal Lehra with adjustable speeds", "Western compositions created on the Octapad, for the Kathakar's own choreography","Learn complex and tongue-twisting compositions to improve your recitation skills, including a phrase-by-phrase breakdown", "Specially curated tutorials and showcases by Guru Sandip Mallick and Guru Aniruddha Mukherjee", "An article detailing the Dasa Prana, the 10 Vital Elements of Taal"]
    
    var tileImages = ["Tabla Image 1", "Ghungroo3", "TG1", "GhungrooEdited2", "Tabla Image 3", "TB8", "Octapad", "Padhant", "Ghungroo1", "TB7"]
    
    
    func  getScale(proxy: GeometryProxy) -> CGFloat {
        var scale: CGFloat = 1
        let x = proxy.frame(in: .global).minX
        let diff = abs(x - 90 * pow(modifiers.wt, 1))
        if diff < 200 * modifiers.wt {
            scale = 1 + (200 * modifiers.wt - diff) / (500 * modifiers.wt)
            
        }
        
//        if scale >= 1.392 {
//            let impactMed = UIImpactFeedbackGenerator(style: .heavy)
//                impactMed.impactOccurred()
//        }
        
        return scale
    }
    

    init() {
        UINavigationBar.appearance().titleTextAttributes = [.foregroundColor: UIColor(named: "gold2") ?? .white]
    }

    fileprivate func tile(_ indexNum: Int, _ scale: CGFloat) -> some View {
        return ZStack{
            Image("TB3")
                .resizable()
                .scaledToFill()
            
            VStack(alignment: .center){
                
                Spacer()
                
                ZStack{
                    Image(tileImages[indexNum])
                        .resizable()
                        .scaledToFill()
                        .scaleEffect(1.2)
                    
                    Text("")
                        .frame(width:190 * modifiers.wt, height: 120 * pow(modifiers.wt,0.8))
                        .background(Color.white3)
                    
                    
                    Text("\(tileTitles[indexNum])")
                        .font(.system(size: 27 * modifiers.wt, weight: .bold, design: .serif))
                        .multilineTextAlignment(.center)
                        .padding(10 * modifiers.wt)
                        .foregroundColor(.black)
                    
                }
                .frame(width: 190 * modifiers.wt, height: 120 * pow(modifiers.wt,0.8))
                .clipped()
                .cornerRadius(20 * modifiers.wt)
                
                Spacer()
                
                Text("\(tileDescriptions[indexNum])")
                    .font(.system(size: 12 * modifiers.wt, weight: .light, design: .serif))
                    .multilineTextAlignment(.center)
                    .padding()
                    .frame(width: 190 * modifiers.wt)
                    .foregroundColor(.black)
                    .background(Color.white1)
                    .cornerRadius(20 * modifiers.wt)
                
                
                Spacer()
            }
            .frame(height: 290 * pow(modifiers.wt,0.8))
            .foregroundColor(Color.white)
            
            
        }
        .frame(width: 220 * modifiers.wt, height: 300 * pow(modifiers.wt,0.8))
        .overlay(
            RoundedRectangle(cornerRadius: 30 * modifiers.ht)
                .stroke(Color.white, lineWidth: 7 * modifiers.ht)
        )
        .cornerRadius(30.0 * modifiers.ht)
        .clipped()
        .shadow(color: .purple, radius: 20 * modifiers.ht, x: 0, y: 10 * modifiers.ht)
        //                                        .rotation3DEffect(
        //                                            .degrees(-Double(proxy.frame(in: .global).minX - (90)) / (11 * modifiers.wt)),
        //                                            axis: (x: 0, y: 0.3, z: 0)
        //                                        )
        .scaleEffect(CGSize(width: scale, height: scale))
    }
    
    var body: some View {
        
        ZStack{
            
            Image("TB6")
                .resizable()
            
            
            VStack {

                Spacer()

//                Text("Offset: \(String(format: "%.2f", horizontalOffset))")
//                    .frame(maxWidth: .infinity)
//                    .padding()
//                    .background(Color.yellow)


                OffsettableScrollView { point in
                    horizontalOffset = (point.x * -1) + 1546.5 * modifiers.wt
                    self.progressValue = Float((horizontalOffset + 100 * modifiers.wt)/((2500 * modifiers.wt)+100 * modifiers.wt))

                } content: {

                    ScrollViewReader { prox in

                        HStack{
                            Text("")
                                .frame(width: 80 * modifiers.wt)

                            HStack(spacing: 90 * pow(modifiers.wt,1.2)) {
                                Group{
                                    GeometryReader { proxy in
                                        NavigationLink(destination: Tabla().onAppear(){let impactMed = UIImpactFeedbackGenerator(style: .light); impactMed.impactOccurred()}){

                                            let scale = getScale(proxy: proxy)
                                            let indexNum = 0

                                            tile(indexNum, scale)
                                        }
                                    }
                                    .frame(width: 200 * modifiers.wt, height: 340 * pow(modifiers.wt,0.8))
                                    .id(0)

                                    GeometryReader { proxy in
                                        NavigationLink(destination: Riyaz().onAppear(){let impactMed = UIImpactFeedbackGenerator(style: .light); impactMed.impactOccurred()}){

                                            let scale = getScale(proxy: proxy)
                                            let indexNum = 1

                                            tile(indexNum, scale)
                                        }
                                    }
                                    .frame(width: 200 * modifiers.wt, height: 340 * pow(modifiers.wt,0.8))
                                    .id(1)

                                    GeometryReader { proxy in
                                        NavigationLink(destination: Kramalaya().onAppear(){let impactMed = UIImpactFeedbackGenerator(style: .light); impactMed.impactOccurred()}){

                                            let scale = getScale(proxy: proxy)
                                            let indexNum = 2

                                            tile(indexNum, scale)

                                        }
                                    }
                                    .frame(width: 200 * modifiers.wt, height: 340 * pow(modifiers.wt,0.8))
                                    .id(2)

                                    GeometryReader { proxy in
                                        NavigationLink(destination: Ladi_Upaj().onAppear(){let impactMed = UIImpactFeedbackGenerator(style: .light); impactMed.impactOccurred()}){

                                            let scale = getScale(proxy: proxy)
                                            let indexNum = 3

                                            tile(indexNum, scale)
                                        }
                                    }
                                    .frame(width: 200 * modifiers.wt, height: 340 * pow(modifiers.wt,0.8))
                                    .id(3)

                                    GeometryReader { proxy in
                                        NavigationLink(destination: Tehai().onAppear(){let impactMed = UIImpactFeedbackGenerator(style: .light); impactMed.impactOccurred()}){

                                            let scale = getScale(proxy: proxy)
                                            let indexNum = 4

                                            tile(indexNum, scale)
                                        }
                                    }
                                    .frame(width: 200 * modifiers.wt, height: 340 * pow(modifiers.wt,0.8))
                                    .id(4)

                                    GeometryReader { proxy in
                                        NavigationLink(destination: Lehra().onAppear(){let impactMed = UIImpactFeedbackGenerator(style: .light); impactMed.impactOccurred()}){

                                            let scale = getScale(proxy: proxy)
                                            let indexNum = 5

                                            tile(indexNum, scale)
                                        }
                                    }
                                    .frame(width: 200 * modifiers.wt, height: 340 * pow(modifiers.wt,0.8))
                                    .id(5)

                                    GeometryReader { proxy in
                                        NavigationLink(destination: Octapad().onAppear(){let impactMed = UIImpactFeedbackGenerator(style: .light); impactMed.impactOccurred()}){

                                            let scale = getScale(proxy: proxy)
                                            let indexNum = 6

                                            tile(indexNum, scale)
                                        }
                                    }
                                    .frame(width: 200 * modifiers.wt, height: 340 * pow(modifiers.wt,0.8))
                                    .id(6)

                                    GeometryReader { proxy in
                                        NavigationLink(destination: Padhant().onAppear(){let impactMed = UIImpactFeedbackGenerator(style: .light); impactMed.impactOccurred()}){

                                            let scale = getScale(proxy: proxy)
                                            let indexNum = 7

                                            tile(indexNum, scale)
                                        }
                                    }
                                    .frame(width: 200 * modifiers.wt, height: 340 * pow(modifiers.wt,0.8))
                                    .id(7)

                                    GeometryReader { proxy in
                                        NavigationLink(destination: Gurus().onAppear(){let impactMed = UIImpactFeedbackGenerator(style: .light); impactMed.impactOccurred()}){

                                            let scale = getScale(proxy: proxy)
                                            let indexNum = 8

                                            tile(indexNum, scale)
                                        }
                                    }
                                    .frame(width: 200 * modifiers.wt, height: 340 * pow(modifiers.wt,0.8))
                                    .id(8)

                                    GeometryReader { proxy in
                                        NavigationLink(destination: DasaPrana().onAppear(){let impactMed = UIImpactFeedbackGenerator(style: .light); impactMed.impactOccurred()}){

                                            let scale = getScale(proxy: proxy)
                                            let indexNum = 9

                                            tile(indexNum, scale)
                                        }
                                    }
                                    .frame(width: 200 * modifiers.wt, height: 340 * pow(modifiers.wt,0.8))
                                    .id(9)

                                }.frame(height: 800 * modifiers.ht)
                            }
                            .onChange(of: leftFlag, perform: { value in
                                withAnimation(.spring()){
                                    prox.scrollTo(0, anchor: .trailing)
                                }
                            })
                            .onChange(of: rightFlag, perform: { value in
                                withAnimation(.spring()){
                                    prox.scrollTo(9, anchor: .center)
                                }
                            })

                            Text("")
                                .frame(width: 100 * modifiers.wt)

                        }
                    }
                }
                Spacer()

            }
                
            
            VStack{
            
                            Spacer()
                                .frame(height: pow(modifiers.ht,0.8) * 30)
            
            
                            HStack{
                                Spacer()
                                    .frame(width: modifiers.wt * 265)
            
                                Button(action: {
                                    withAnimation(.spring()){
                                        self.showInfo.toggle()
                                    }
                                    let impactMed = UIImpactFeedbackGenerator(style: .heavy)
                                    impactMed.impactOccurred()
                                }
                                ) {
                                    Image(systemName: self.showInfo ? "xmark" : "info.circle")
                                        .resizable()
                                        .foregroundColor(.white)
                                        .frame(width: modifiers.ht * 35, height: modifiers.ht * 35)
                                }
                            }
            
                            Spacer()
                                .frame(height: modifiers.ht * 15)
            
                            HStack{
            
                                Spacer()
                                    .frame(width: modifiers.wt * 230)
            
                                if self.showInfo{
                                    InfoPopUp()
                                }
                            }
            
            
                            Spacer()
            
                        }
                        .padding()
            
            VStack{
                
                Spacer()
                
                
                HStack{
                    
                    Spacer()
                    
                    Button(action: {
                        
                        leftFlag = !leftFlag
                        
                        let impactMed = UIImpactFeedbackGenerator(style: .medium)
                        impactMed.impactOccurred()
                    }
                    ) {
                        Image(systemName: "arrow.left")
                            .resizable()
                            .foregroundColor(.white)
                            .frame(width: modifiers.wt * 20, height: modifiers.wt * 15)
                    }
                    
                    Spacer()
                    
                    ProgressBar(value: withAnimation{$progressValue}).frame(width: 280 * modifiers.wt, height: 20 * modifiers.wt)
                    
                    Spacer()
                     
                    Button(action: {
                        
                        rightFlag = !rightFlag

                        let impactMed = UIImpactFeedbackGenerator(style: .medium)
                        impactMed.impactOccurred()
                    }
                    ) {
                        Image(systemName: "arrow.right")
                            .resizable()
                            .foregroundColor(.white)
                            .frame(width: modifiers.wt * 20, height: modifiers.wt * 15)
                    }
                    
                    Spacer()
                    
                    
                }
                
                    Spacer()
                    .frame(height: 40 * modifiers.ht)
            }
            
            
            
        }
        .navigationBarBackButtonHidden(true).navigationBarTitle("Home", displayMode: .inline).navigationBarHidden(true)
        .onAppear{
                    showInfo.self = false
        }
    }
}


struct InfoPopUp : View {
    
    @StateObject var modifiers = ScreenModifiers()
    
    var body : some View{
        
        VStack(alignment: .leading, spacing: modifiers.ht * 7) {
            
            NavigationLink(destination: About().onAppear(){let impactMed = UIImpactFeedbackGenerator(style: .light); impactMed.impactOccurred()}){
                
                HStack{
                    Text("About")
                        .font(.custom("BodoniSvtyTwoOSITCTT-Bold", size: modifiers.wt * 18))
                }
            }
            
            Divider()
                .frame(height: modifiers.ht * 1)
                .background(Color.black)

            NavigationLink(destination: Credits().onAppear(){let impactMed = UIImpactFeedbackGenerator(style: .light); impactMed.impactOccurred()}){
                
                HStack{

                    Text("Credits")
                        .font(.custom("BodoniSvtyTwoOSITCTT-Bold", size: modifiers.wt * 18))
                }
            }

            
            Divider()
                .frame(height: modifiers.ht * 1)
                .background(Color.black)
            
            
            NavigationLink(destination: Contact().onAppear(){let impactMed = UIImpactFeedbackGenerator(style: .light); impactMed.impactOccurred()}){
                
                HStack{
                    Text("Contact Us")
                        .font(.custom("BodoniSvtyTwoOSITCTT-Bold", size: modifiers.wt * 18))
                }
            }
            
            
            Divider()
                .frame(height: modifiers.ht * 1)
                .background(Color.black)
            
            NavigationLink(destination: PrivacyPolicy().onAppear(){let impactMed = UIImpactFeedbackGenerator(style: .light); impactMed.impactOccurred()}){
                
                HStack{
                    Text("Privacy Policy")
                        .font(.custom("BodoniSvtyTwoOSITCTT-Bold", size: modifiers.wt * 18))
                }
            }
        }
        .frame(width: modifiers.wt * 105)
        .padding(modifiers.wt * 15)
        .foregroundColor(.black)
        .background(Color.gold2)
        .cornerRadius(modifiers.wt *  15)
        
    }
}








