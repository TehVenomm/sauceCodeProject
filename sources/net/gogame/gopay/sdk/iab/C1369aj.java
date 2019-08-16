package net.gogame.gopay.sdk.iab;

import android.view.View;
import android.widget.AdapterView;
import android.widget.AdapterView.OnItemLongClickListener;

/* renamed from: net.gogame.gopay.sdk.iab.aj */
final class C1369aj implements OnItemLongClickListener {

    /* renamed from: a */
    final /* synthetic */ PurchaseActivity f1059a;

    C1369aj(PurchaseActivity purchaseActivity) {
        this.f1059a = purchaseActivity;
    }

    public final boolean onItemLongClick(AdapterView adapterView, View view, int i, long j) {
        if (!
        /*  JADX ERROR: Method code generation error
            jadx.core.utils.exceptions.CodegenException: Error generate insn: 0x0004: INVOKE  (r0v1 boolean) = (wrap: net.gogame.gopay.sdk.iab.PurchaseActivity
              0x0002: IGET  (r0v0 net.gogame.gopay.sdk.iab.PurchaseActivity) = (r3v0 'this' net.gogame.gopay.sdk.iab.aj A[THIS]) net.gogame.gopay.sdk.iab.aj.a net.gogame.gopay.sdk.iab.PurchaseActivity) net.gogame.gopay.sdk.iab.PurchaseActivity.s(net.gogame.gopay.sdk.iab.PurchaseActivity):boolean type: STATIC in method: net.gogame.gopay.sdk.iab.aj.onItemLongClick(android.widget.AdapterView, android.view.View, int, long):boolean, dex: classes.dex
            	at jadx.core.codegen.InsnGen.makeInsn(InsnGen.java:245)
            	at jadx.core.codegen.InsnGen.addArg(InsnGen.java:105)
            	at jadx.core.codegen.ConditionGen.wrap(ConditionGen.java:95)
            	at jadx.core.codegen.ConditionGen.addCompare(ConditionGen.java:123)
            	at jadx.core.codegen.ConditionGen.add(ConditionGen.java:57)
            	at jadx.core.codegen.ConditionGen.wrap(ConditionGen.java:84)
            	at jadx.core.codegen.ConditionGen.addAndOr(ConditionGen.java:151)
            	at jadx.core.codegen.ConditionGen.add(ConditionGen.java:70)
            	at jadx.core.codegen.ConditionGen.add(ConditionGen.java:46)
            	at jadx.core.codegen.RegionGen.makeIf(RegionGen.java:136)
            	at jadx.core.codegen.RegionGen.makeRegion(RegionGen.java:62)
            	at jadx.core.codegen.RegionGen.makeSimpleRegion(RegionGen.java:92)
            	at jadx.core.codegen.RegionGen.makeRegion(RegionGen.java:58)
            	at jadx.core.codegen.MethodGen.addRegionInsns(MethodGen.java:210)
            	at jadx.core.codegen.MethodGen.addInstructions(MethodGen.java:203)
            	at jadx.core.codegen.ClassGen.addMethod(ClassGen.java:316)
            	at jadx.core.codegen.ClassGen.addMethods(ClassGen.java:262)
            	at jadx.core.codegen.ClassGen.addClassBody(ClassGen.java:225)
            	at jadx.core.codegen.ClassGen.addClassCode(ClassGen.java:110)
            	at jadx.core.codegen.ClassGen.makeClass(ClassGen.java:76)
            	at jadx.core.codegen.CodeGen.wrapCodeGen(CodeGen.java:44)
            	at jadx.core.codegen.CodeGen.generateJavaCode(CodeGen.java:32)
            	at jadx.core.codegen.CodeGen.generate(CodeGen.java:20)
            	at jadx.core.ProcessClass.process(ProcessClass.java:36)
            	at jadx.api.JadxDecompiler.processClass(JadxDecompiler.java:311)
            	at jadx.api.JavaClass.decompile(JavaClass.java:62)
            	at jadx.api.JadxDecompiler.lambda$appendSourcesSave$0(JadxDecompiler.java:217)
            Caused by: java.util.ConcurrentModificationException
            	at java.base/java.util.ArrayList.removeIf(ArrayList.java:1714)
            	at java.base/java.util.ArrayList.removeIf(ArrayList.java:1689)
            	at jadx.core.dex.instructions.args.SSAVar.removeUse(SSAVar.java:86)
            	at jadx.core.dex.instructions.args.SSAVar.use(SSAVar.java:79)
            	at jadx.core.dex.nodes.InsnNode.attachArg(InsnNode.java:87)
            	at jadx.core.dex.nodes.InsnNode.addArg(InsnNode.java:73)
            	at jadx.core.dex.nodes.InsnNode.copyCommonParams(InsnNode.java:335)
            	at jadx.core.dex.instructions.IndexInsnNode.copy(IndexInsnNode.java:24)
            	at jadx.core.dex.instructions.IndexInsnNode.copy(IndexInsnNode.java:9)
            	at jadx.core.dex.nodes.InsnNode.copyCommonParams(InsnNode.java:333)
            	at jadx.core.dex.nodes.InsnNode.copy(InsnNode.java:350)
            	at jadx.core.codegen.InsnGen.inlineMethod(InsnGen.java:880)
            	at jadx.core.codegen.InsnGen.makeInvoke(InsnGen.java:669)
            	at jadx.core.codegen.InsnGen.makeInsnBody(InsnGen.java:357)
            	at jadx.core.codegen.InsnGen.makeInsn(InsnGen.java:223)
            	... 26 more
            */
        /*
            this = this;
            r2 = 241(0xf1, float:3.38E-43)
            net.gogame.gopay.sdk.iab.PurchaseActivity r0 = r3.f1059a
            boolean r0 = r0.f1020K
            if (r0 == 0) goto L_0x002b
            net.gogame.gopay.sdk.iab.PurchaseActivity r0 = r3.f1059a
            boolean r0 = r0.f1019J
            if (r0 != 0) goto L_0x002b
            net.gogame.gopay.sdk.iab.PurchaseActivity r0 = r3.f1059a
            boolean r0 = r0.f1021L
            if (r0 == 0) goto L_0x002b
            net.gogame.gopay.sdk.iab.PurchaseActivity r0 = r3.f1059a
            net.gogame.gopay.sdk.iab.ak r1 = new net.gogame.gopay.sdk.iab.ak
            r1.<init>(r3, r6, r5)
            net.gogame.gopay.sdk.iab.al r2 = new net.gogame.gopay.sdk.iab.al
            r2.<init>(r3)
            new android.app.AlertDialog.Builder(r0).setTitle(r0.f1041o).setMessage(r0.f1040n).setPositiveButton(r0.f1038l, r1).setNegativeButton(r0.f1039m, r2).setCancelable(false).show()
        L_0x0029:
            r0 = 1
            return r0
        L_0x002b:
            net.gogame.gopay.sdk.iab.PurchaseActivity r0 = r3.f1059a
            r1 = 0
            r0.f1019J = r1
            net.gogame.gopay.sdk.iab.PurchaseActivity r0 = r3.f1059a
            net.gogame.gopay.sdk.iab.i r0 = r0.f1012C
            r0.f1105e = r6
            net.gogame.gopay.sdk.iab.PurchaseActivity r0 = r3.f1059a
            net.gogame.gopay.sdk.iab.i r0 = r0.f1012C
            android.view.View r0 = r0.f1106f
            if (r0 == 0) goto L_0x0052
            net.gogame.gopay.sdk.iab.PurchaseActivity r0 = r3.f1059a
            net.gogame.gopay.sdk.iab.i r0 = r0.f1012C
            android.view.View r0 = r0.f1106f
            int r1 = android.graphics.Color.rgb(r2, r2, r2)
            r0.setBackgroundColor(r1)
        L_0x0052:
            net.gogame.gopay.sdk.iab.PurchaseActivity r0 = r3.f1059a
            net.gogame.gopay.sdk.iab.i r0 = r0.f1012C
            r0.f1106f = r5
            r0 = -1
            r5.setBackgroundColor(r0)
            net.gogame.gopay.sdk.iab.PurchaseActivity r1 = r3.f1059a
            net.gogame.gopay.sdk.iab.PurchaseActivity r0 = r3.f1059a
            net.gogame.gopay.sdk.iab.i r0 = r0.f1012C
            java.lang.Object r0 = r0.getItem(r6)
            net.gogame.gopay.sdk.iab.a r0 = (net.gogame.gopay.sdk.iab.C1365a) r0
            r1.m803a(r0)
            goto L_0x0029
        */
        throw new UnsupportedOperationException("Method not decompiled: net.gogame.gopay.sdk.iab.C1369aj.onItemLongClick(android.widget.AdapterView, android.view.View, int, long):boolean");
    }
}
