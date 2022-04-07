//
//  Tehai.swift
//  Tabla App
//
//  Created by Swagnik Roychoudhury on 9/5/21.
//
import SwiftUI
import AVKit
import Foundation


struct Tehai: View {
    
    var videoID01 = "4mjELUnl_Ps"
    var videoID02 = "-G_b531hT4w"
    var videoID03 = "MLeFcdU9NAQ"
    var videoID04 = "remKi3MJAiM"
    var videoID05 = "WUWoOLwvoJ8"
    var videoName01 = "Introduction"
    var videoName02 = "Simple Tehai"
    var videoName03 = "Bedam Tehai"
    var videoName04 = "Kamali Chakradhar"
    var videoName05 = "Farmaishi Chakradhar"
    
    @StateObject var modifiers = ScreenModifiers()
    
    
    var body: some View
    {
        //SampleVideoPlayer2()
        
        //NavigationView{
        
        ZStack
        {
            LinearGradient(gradient: Gradient(colors: [Color.blue2, Color.grey2, Color.grey2]), startPoint: .top, endPoint: .bottom).ignoresSafeArea()
            VStack
            {
                Spacer()
                    .frame(height:30)
                
                Image("Tabla Image 3")
                    .resizable()
                    .frame(width: modifiers.ht * 275, height: modifiers.ht * 178)
                    .border(Color.black, width: 5)
                
                Spacer()
                    .frame(height: modifiers.ht * 30)
                
                Text("Tehai")
                    .foregroundColor(.gold2)
                    .font(.system(size: modifiers.wt * 24, weight: .bold))
                    .frame(width: modifiers.wt * 100, height: modifiers.ht * 25, alignment: .center)
                    .padding()
                    .background(Color.grey2)
                    .border(Color.gold2, width: /*@START_MENU_TOKEN@*/1/*@END_MENU_TOKEN@*/)
                    //.cornerRadius(20)
                    
                    //Text("Subtitle")
                    .foregroundColor(.white)
                Spacer()
                
            }
            
            ScrollView
            {
                VStack(spacing:0)
                {
                    HStack
                    {
                        Spacer()
                            .frame(height: modifiers.ht * 360)
                            .background(LinearGradient(gradient: Gradient(colors: [Color.clear, Color.clear, Color.clear, Color.clear, Color.grey2]), startPoint: .top, endPoint: .bottom))
                    }
                    
                    HStack
                    {
                        Text("Purpose")
                            .fontWeight(.bold)
                            .font(.system(size: modifiers.wt * 30))
                        
                        Spacer()
                        
                    }
                    .padding(10)
                    .background(Color.grey2)
                    .foregroundColor(.gold2)
                    .border(width: modifiers.wt * 2, edges: [.bottom], color: .gold2)
                    .padding(modifiers.wt * 20)
                    .background(Color.grey2)
                    
                    HStack{
                        
                        Text("These videos explore the mathematical structures of different types of tehai. Use the calculators below to create your own tehais. ")
                            .fixedSize(horizontal: false, vertical: true)
                            .multilineTextAlignment(.center)
                            .frame(width: modifiers.wt * 300)
                            .font(.system(size: modifiers.wt * 16))
                            .foregroundColor(.gold2)
                            .padding(modifiers.wt * 20)
                            .background(Color.black1)
                            .cornerRadius(15)
                        
                    }
                    .frame(width: modifiers.wt * 390)
                    .background(Color.grey2)
                    
                    HStack
                    {
                        Text("Mathematics of Tehai")
                            .fontWeight(.bold)
                            .font(.system(size: modifiers.wt * 30))
                        
                        Spacer()
                        
                    }
                    .padding(10)
                    .background(Color.grey2)
                    .foregroundColor(.gold2)
                    .border(width: modifiers.ht * 2, edges: [.bottom], color: .gold2)
                    .padding(modifiers.wt * 20)
                    .background(Color.grey2)
                    
                    
                    
                    ForEach(0..<5){video in
                        VStack{
                            if(video == 0){
                                VideoList(videoID: videoID01, videoName: videoName01)
                            }
                            
                            else if(video == 1){
                                VideoList(videoID: videoID02, videoName: videoName02)
                            }
                            
                            else if(video == 2){
                                VideoList(videoID: videoID03, videoName: videoName03)
                            }
                            
                            else if(video == 3){
                                VideoList(videoID: videoID04, videoName: videoName04)
                            }
                            
                            else if(video == 4){
                                VideoList(videoID: videoID05, videoName: videoName05)
                            }
                        }
                    }
                    
                    
                    HStack
                    {
                        Text("Tehai Calculators")
                            .fontWeight(.bold)
                            .font(.system(size: modifiers.wt * 30))
                        
                        Spacer()
                        
                    }
                    .padding(10)
                    .background(Color.grey2)
                    .foregroundColor(.gold2)
                    .border(width: modifiers.ht * 2, edges: [.bottom], color: .gold2)
                    .padding(modifiers.wt * 20)
                    .background(Color.grey2)
                    
                    
                    
                    ForEach(0..<3){item in
                        VStack{
                            
                            if(item == 0){
                                NavigationLink(destination: SimpleTehai()){
                                    ItemList(itemName: "Simple Tehai")
                                }
                            }
                            
                            else if(item == 1){
                                NavigationLink(destination: Kamali()){
                                    ItemList(itemName: "Kamali Chakradhar")
                                }
                            }
                            
                            else if(item == 2){
                                NavigationLink(destination: Farmaishi()){
                                    ItemList(itemName: "Farmaishi Chakradhar")
                                }
                            }
                            
                            
                            
                        }
                    }
                }
            }
        }
    }
    
    
}

struct SimpleTehai: View {
    
    @StateObject var modifiers = ScreenModifiers()
    
    @State private var isExpanded = false
    
    @State private var tType = 0
    
    @State var rawMaatra: String = ""
    
    @State var rawAvartaan: String = ""
    
    @State var num1: Double = 0
    
    @State var num2: Double = 0
    
    @State var check1: Double = 0
    
    @State var p1: Int = 0
    
    @State var g1: Double = 0
    
    @State var str_p1: String = ""
    
    @State var str_g1: String = ""
    
    @State var avartan: Int = 0
    
    @State var str_avtn: String = ""
    
    
    var body: some View {
        
        
        
        ZStack {
            
            
            
            //Color.grey1.ignoresSafeArea()
            
            Image("Ghungroo3").centerCropped()
            
            
            
            VStack {
                
                Group{
                    Spacer()
                    Spacer()
                }
                
                
                Group{
                    Text("Number of Maatras in Taal:")
                        .fontWeight(.bold)
                        .font(.system(size: modifiers.wt * 20))
                        .foregroundColor(.gold2)
                    
                    TextField("Maatras", text: $rawMaatra)
                        .keyboardType(.numberPad)
                        .font(.system(size: modifiers.wt * 15))
                        .frame(width: modifiers.wt * 150, height: modifiers.ht * 50,
                               alignment: .center)
                        .textFieldStyle(RoundedBorderTextFieldStyle())
                        .foregroundColor(.gold2)
                }
                
                Group{
                    Text("Number of Avartaans in Tehai:")
                        .fontWeight(.bold)
                        .font(.system(size: modifiers.wt * 20))
                        .foregroundColor(.gold2)
                    
                    TextField("Avartaans", text: $rawAvartaan)
                        .keyboardType(.numberPad)
                        .font(.system(size: modifiers.wt * 15))
                        .frame(width: modifiers.wt * 150, height: modifiers.ht * 30,
                               alignment: .center)
                        .textFieldStyle(RoundedBorderTextFieldStyle())
                        .foregroundColor(.gold2)
                }
                
                Spacer()
                
                Button{
                    
                    num1.self = Double(rawMaatra.self) ?? 0
                    
                    num2.self = Double(rawAvartaan.self) ?? 0
                    
                    check1 = Double(Int(num1.self) % 3)
                    
                    
                    p1 = Int((num1.self * num2.self + 1)/3)
                    
                    g1 = ((num1.self * num2.self + 1).truncatingRemainder(dividingBy: 3))/2
                    
                    
                    str_p1 = String(p1)
                    str_g1 = String(g1)

                    
                    hideKeyboard()
                    
                    
                }   label: {
                    
                    Text("Calculate")
                        .fontWeight(/*@START_MENU_TOKEN@*/.bold/*@END_MENU_TOKEN@*/)
                        
                        .font(.system(size: modifiers.wt * 20))
                        
                        .padding()
                        
                        .foregroundColor(.grey1)
                        
                        .background(Color.gold2)
                        
                        .cornerRadius(20)
                        
                        .frame(maxWidth: modifiers.wt * 280)
                    
                }
                
                Spacer()
                
                VStack {
                    
                    Spacer()
                    
                    Text("Palla size: \(str_p1)")
                        .fontWeight(/*@START_MENU_TOKEN@*/.bold/*@END_MENU_TOKEN@*/)
                        .font(.system(size: modifiers.wt * 25))
                        .padding()
                        .foregroundColor(.gold2)
                    
                    Spacer()
                    
                    Text("Gap size: \(str_g1)")
                        .fontWeight(/*@START_MENU_TOKEN@*/.bold/*@END_MENU_TOKEN@*/)
                        .font(.system(size: modifiers.wt * 25))
                        .padding()
                        .foregroundColor(.gold2)
                    
                    Spacer()
                    
                }
                .padding()
                .frame(maxWidth: modifiers.wt * 300, maxHeight: modifiers.ht * 250)
                .background(Color.black3)
                .cornerRadius(30)
//                .border(Color.gold2, width: modifiers.wt * 5)
                
                
                
                Group{
                    
                    Spacer()
                    Spacer()
                    
                }
                
                
                
            }
            
            
            
        }.navigationBarTitle("Simple Tehai Calculator", displayMode: .inline)
        
    }
    
}

struct Kamali: View {
    
    
    @StateObject var modifiers = ScreenModifiers()
    
    var maatraList = [7,10,12,14,15,16]
    
    
    @State var raw: String = ""
    
    @State var num: Double = 0
    
    
    
    @State var check1: Double = 0
    
    @State var p1: Int = 0
    
    @State var p2: Int = 0
    
    @State var tb1: Int = 0
    
    @State var t1: Double = 0
    
    @State var g1: Int = 0
    
    @State var str_p1: String = ""
    
    @State var str_p2: String = ""
    
    @State var str_tb1: String = ""
    
    @State var str_t1: String = ""
    
    @State var str_g1: String = ""
    
    @State private var isExpanded = false
    
    @State private var taal = 16
    
    @State var showHelp = false
    
    
    var body: some View {
        
        
        
        ZStack {
            
            
            
            
            Image("Ghungroo3").centerCropped()
            
            
            
            VStack {
                
                Group{
                    Spacer()
                    Spacer()
                }
                
                
                
                Text("Number of Maatras in Taal:")
                    .fontWeight(/*@START_MENU_TOKEN@*/.bold/*@END_MENU_TOKEN@*/)
                    .font(.system(size: modifiers.wt * 25))
                    .padding()
                    .foregroundColor(.gold2)
                
                VStack(alignment: .leading, spacing: modifiers.wt * 15)
                {
                    HStack{
                        
                        Spacer()
                        
                        DisclosureGroup("\(taal)", isExpanded: $isExpanded) {
                            VStack{
                                ForEach(0...maatraList.count-1, id: \.self)
                                {
                                    num in
                                    
                                    Text("\(maatraList[num])")
                                        .font(.system(size: modifiers.wt * 15))
                                        .padding(0)
                                        .onTapGesture {
                                            self.taal = maatraList[num]
                                            
                                            withAnimation {
                                                self.isExpanded.toggle()
                                            }
                                        }
                                }
                            }
                        }
                        .accentColor(.gold2)
                        .foregroundColor(.gold2)
                        .font(.system(size: modifiers.wt * 17))
                        
                        Spacer()
                        
                    }
                    .font(.system(size: modifiers.wt * 25))
                    .background(Color.black)
                    .cornerRadius(8)
                    .frame(maxWidth: modifiers.wt * 70)
                    //.padding(.horizontal, (1/modifiers.wt) * 150)
                    
                }
                
                
                
                Spacer()
                
                Button{
                    
                    num.self = Double(taal)
                    
                    if(taal == 7)
                    {
                        p1 = 5
                        p2 = 12
                        tb1 = 2
                        t1 = 1.5
                        g1 = 1
                    }
                    
                    else if(taal == 10)
                    {
                        p1 = 8
                        p2 = 18
                        tb1 = 2
                        t1 = 3
                        g1 = 1
                    }
                    
                    else if(taal == 12)
                    {
                        p1 = 10
                        p2 = 22
                        tb1 = 2
                        t1 = 3
                        g1 = 5
                    }
                    
                    else if(taal == 14)
                    {
                        p1 = 9
                        p2 = 23
                        tb1 = 5
                        t1 = 3
                        g1 = 0
                    }
                    
                    else if(taal == 15)
                    {
                        p1 = 11
                        p2 = 26
                        tb1 = 4
                        t1 = 3
                        g1 = 5
                    }
                    
                    else if(taal == 16)
                    {
                        p1 = 10
                        p2 = 26
                        tb1 = 6
                        t1 = 3
                        g1 = 1
                    }
                    
                    
                    str_p1 = String(p1)
                    str_p2 = String(p2)
                    str_tb1 = String(tb1)
                    str_t1 = String(t1)
                    str_g1 = String(g1)
                    
                    hideKeyboard()
                    
                    
                }   label: {
                    
                    Text("Calculate")
                        .fontWeight(/*@START_MENU_TOKEN@*/.bold/*@END_MENU_TOKEN@*/)
                        
                        .font(.system(size: modifiers.wt * 20))
                        
                        .padding()
                        
                        .foregroundColor(.grey1)
                        
                        .background(Color.gold2)
                        
                        .cornerRadius(20)
                        
                        .frame(maxWidth: modifiers.wt * 280)
                    
                }
                
                Spacer()
                
                VStack {
                    
                    Spacer()
                    
                    Text("Part A: \(str_p1)")
                        .fontWeight(/*@START_MENU_TOKEN@*/.bold/*@END_MENU_TOKEN@*/)
                        .font(.system(size: modifiers.wt * 20))
                        .padding()
                        .foregroundColor(.gold2)
                    
                    Spacer()
                    
                    Text("Part B: \(str_tb1)")
                        .fontWeight(/*@START_MENU_TOKEN@*/.bold/*@END_MENU_TOKEN@*/)
                        .font(.system(size: modifiers.wt * 20))
                        .padding()
                        .foregroundColor(.gold2)
                    
                    Spacer()
                    
                    Text("Part C: \(str_t1)")
                        .fontWeight(/*@START_MENU_TOKEN@*/.bold/*@END_MENU_TOKEN@*/)
                        .font(.system(size: modifiers.wt * 20))
                        .padding()
                        .foregroundColor(.gold2)
                    
                    Spacer()
                    
                    Text("Gap size: \(str_g1)")
                        .fontWeight(/*@START_MENU_TOKEN@*/.bold/*@END_MENU_TOKEN@*/)
                        .font(.system(size: modifiers.wt * 20))
                        .padding()
                        .foregroundColor(.gold2)
                    
                    Spacer()
                    
                }
                .padding()
                .frame(maxWidth: modifiers.wt * 300, maxHeight: modifiers.ht * 350)
                .background(Color.black3)
                .cornerRadius(30)
//                .border(Color.gold2, width: modifiers.wt * 5)
                
                Group{
                    Spacer()
                    Spacer()
                }
                
                
                
                
                
            }
            
            
            VStack{
                
                Spacer()
                    .frame(height: modifiers.ht * 35)
                
                
                HStack{
                    Spacer()
                        .frame(width: modifiers.wt * 290)
                    
                    Button(action: {
                        withAnimation(.spring()){
                            self.showHelp.toggle()
                        }
                    }
                    ) {
                        Image(systemName: self.showHelp ? "xmark.circle" : "questionmark.circle")
                            .resizable()
                            .foregroundColor(.gold2)
                            .frame(width: modifiers.ht * 30, height: modifiers.ht * 30)
                    }
                }
                
                Spacer()
                    .frame(height: modifiers.ht * 15)
                
                HStack{
                    
                    Spacer()
                        .frame(width: modifiers.wt * 100)
                    
                    if self.showHelp{
                        HelpPopUp(tehaiType: "Kamali")
                    }
                }

                
                Spacer()
            
            }
            .padding()
            
            
            
        }.navigationBarTitle("Kamali Calculator", displayMode: .inline)
        
    }
    
}

struct Farmaishi: View {
    
    @StateObject var modifiers = ScreenModifiers()
    
    @State var raw: String = ""
    
    @State var num: Double = 0
    
    
    
    @State var check1: Double = 0
    
    @State var p1: Double = 0
    
    @State var p2: Double = 0
    
    @State var p3: Double = 0
    
    @State var t1: Double = 0
    
    @State var g1: Double = 0
    
    @State var str_p1: String = ""
    
    @State var str_p2: String = ""
    
    @State var str_p3: String = ""
    
    @State var str_t1: String = ""
    
    @State var str_g1: String = ""
    
    @State var showHelp = false
    
    
    var body: some View {
        
        
        
        ZStack {
            
            
            
            //Color.grey1.ignoresSafeArea()
            
            Image("Ghungroo3").centerCropped()
            
            
            
            
            VStack {
                
                Group{
                    Spacer()
                    Spacer()
                }
                
                
                
                Text("Number of Maatras in Taal:")
                    .fontWeight(/*@START_MENU_TOKEN@*/.bold/*@END_MENU_TOKEN@*/)
                    .font(.system(size: modifiers.wt * 25))
                    .padding()
                    .foregroundColor(.gold2)
                
                TextField("Maatras", text: $raw)
                    .keyboardType(.numberPad)
                    .font(.system(size: modifiers.wt * 15))
                    .frame(width: modifiers.wt * 150, height: modifiers.ht * 30,
                           alignment: /*@START_MENU_TOKEN@*/.center/*@END_MENU_TOKEN@*/)
                    .textFieldStyle(RoundedBorderTextFieldStyle())
                    .foregroundColor(.gold2)
                
                Spacer()
                
                Button{
                    
                    num.self = Double(raw.self) ?? 0
                    
                    check1 = Double(Int(num.self) % 3)
                    
                    if (check1 == 0){
                        
                        t1 = ((((5*num.self + 1) - 2*2)/3) - (num.self + 1)) / 2;
                        
                        p1 = (num.self + 1) - t1;
                        
                        p2 = p1 + num.self;
                        
                        p3 = p2 + num.self;
                        
                        g1 = 2
                        
                    }
                    
                    else if (check1 == 1){
                        
                        p1 = ((num.self * Double(5)) / 3) - (num.self - (Double (4) / 3));
                        
                        t1 = ((num.self * 5 + 1) - (3 * p1)) / 9
                        
                        p2 = p1 + num.self
                        
                        p3 = p2 + num.self
                        
                        g1 = 0
                        
                    }
                    
                    else if (check1 == 2){
                        
                        p1 = ((num.self * Double(5)) / 3) - (num.self - (Double(5) / 3))
                        
                        t1 = ((num.self * 5 - 1) - (3 * p1)) / 9
                        
                        p2 = p1 + num.self
                        
                        p3 = p2 + num.self
                        
                        g1 = 1
                        
                    }
               
                    
                    str_p1 = String(Int(p1 + 0.5))
                    str_p2 = String(Int(p2 + 0.5))
                    str_p3 = String(Int(p3 + 0.5))
                    str_t1 = String(Int(t1 + 0.5))
                    str_g1 = String(Int(g1 + 0.5))
                    
                    hideKeyboard()
                    
                    
                }   label: {
                    
                    Text("Calculate")
                        .fontWeight(/*@START_MENU_TOKEN@*/.bold/*@END_MENU_TOKEN@*/)
                        
                        .font(.system(size: modifiers.wt * 20))
                        
                        .padding()
                        
                        .foregroundColor(.grey1)
                        
                        .background(Color.gold2)
                        
                        .cornerRadius(20)
                        
                        .frame(maxWidth: modifiers.wt * 280)
                    
                }
                
                Spacer()
                
                VStack {
                    
                    Spacer()
                    
                    Text("Part A: \(str_p1) \(str_p2) \(str_p3)")
                        .fontWeight(/*@START_MENU_TOKEN@*/.bold/*@END_MENU_TOKEN@*/)
                        .font(.system(size: modifiers.wt * 25))
                        .padding()
                        .foregroundColor(.gold2)
                    
                    Spacer()
                    
                    Text("Part B: \(str_t1)")
                        .fontWeight(/*@START_MENU_TOKEN@*/.bold/*@END_MENU_TOKEN@*/)
                        .font(.system(size: modifiers.wt * 25))
                        .padding()
                        .foregroundColor(.gold2)
                    
                    Spacer()
                    
                    Text("Gap size: \(str_g1)")
                        .fontWeight(/*@START_MENU_TOKEN@*/.bold/*@END_MENU_TOKEN@*/)
                        .font(.system(size: modifiers.wt * 25))
                        .padding()
                        .foregroundColor(.gold2)
                    
                }
                .padding()
                .frame(maxWidth: modifiers.wt * 300, maxHeight: modifiers.ht * 250)
                .background(Color.black3)
                .cornerRadius(30)
//                .border(Color.gold2, width: modifiers.wt * 5)
                
                Group{
                    Spacer()
                    Spacer()
                    Spacer()
                }
                
                
                
                
                
            }
            
            VStack{
                
                Spacer()
                    .frame(height: modifiers.ht * 35)
                
                
                HStack{
                    Spacer()
                        .frame(width: modifiers.wt * 290)
                    
                    Button(action: {
                        withAnimation(.spring()){
                            self.showHelp.toggle()
                        }
                    }
                    ) {
                        Image(systemName: self.showHelp ? "xmark.circle" : "questionmark.circle")
                            .resizable()
                            .foregroundColor(.gold2)
                            .frame(width: modifiers.ht * 30, height: modifiers.ht * 30)
                    }
                }
                
                Spacer()
                    .frame(height: modifiers.ht * 15)
                
                HStack{
                    
                    Spacer()
                        .frame(width: modifiers.wt * 100)
                    
                    if self.showHelp{
                        HelpPopUp(tehaiType: "Farmaishi")
                    }
                }

                
                Spacer()
            
            }
            .padding()
            
            
            
        }.navigationBarTitle("Farmaishi Calculator", displayMode: .inline)
        
    }
    
}

struct Tehai_Previews: PreviewProvider {
    
    static var previews: some View {
        
        Group {
            Farmaishi().preferredColorScheme(.dark)
            
            
        }
        
    }
    
}


struct HelpPopUp : View {
    
    @StateObject var modifiers = ScreenModifiers()
    
    var tehaiType: String
    
    var body : some View{
        
        VStack(alignment: .leading, spacing: modifiers.ht * 5) {
            
            
            
            Text("Refer to the \(tehaiType) video in the \"Mathematics of Tehai\" to learn what different part names mean.")
                .font(.system(size: modifiers.wt * 18))
                .padding()
            
            
        }
        .frame(width: modifiers.wt * 230)
        .padding(modifiers.wt * 10)
        .foregroundColor(.gold2)
        .background(Color.black2)
        .cornerRadius(modifiers.wt *  10)
        
    }
}


