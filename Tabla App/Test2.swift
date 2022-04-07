////
////  Test1.swift
////  Tabla App
////
////  Created by Swagnik Roychoudhury on 4/6/22.
////
//
//
//import SwiftUI
//
//struct Test2View: View {
//
//
//    private func  getScale(proxy: GeometryProxy) -> CGFloat {
//        var scale: CGFloat = 1
//        let x = proxy.frame(in: .global).minX
//        let diff = abs(x - 120)
//        if diff < 200 {
//            scale = 1 + (200 - diff) / 500
//        }
//
//        if scale == 1.4 {
//            let impactMed = UIImpactFeedbackGenerator(style: .medium)
//                impactMed.impactOccurred()
//        }
//
//        return  scale
//    }
//
//    @State var currentIndex: Int = 0
//
//
//    var body: some View  {
//
//
//        SnapCarousel(index: $currentIndex)
//    }
//
//
//}
//
//
//
//struct SnapCarousel<Content: View,T: Identifiable>: View {
//
//    var content: (T) -> Content
//    var list: [T]
//
//    var spacing: CGFloat
//    var trailingSpace: CGFloat
//    @Binding var index: Int
//
//    init(spacing: CGFloat = 15, trailingSpace: CGFloat = 100, index: Binding<Int>, items: [T], @ViewBuilder content: @escaping (T)->Content){
//
//        self.list = items
//        self.spacing = spacing
//        self.trailingSpace = trailingSpace
//        self._index = index
//        self.content = content
//    }
//
//
//    var body : some View{
//
//        GeometryReader{ proxy in
//
//            Content
//
//        }
//
//    }
//
//
//}
//
//
//struct Test2_Previews: PreviewProvider {
//    static var previews: some View {
//        Test2View()
//    }
//}
