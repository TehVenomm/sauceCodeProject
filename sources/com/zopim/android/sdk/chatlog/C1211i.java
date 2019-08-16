package com.zopim.android.sdk.chatlog;

import android.content.Context;
import android.content.res.Resources.NotFoundException;
import android.support.v7.widget.RecyclerView.Adapter;
import android.support.v7.widget.RecyclerView.ViewHolder;
import android.util.Log;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import com.zopim.android.sdk.C1122R;
import com.zopim.android.sdk.api.Chat;
import java.util.Collections;
import java.util.List;

/* renamed from: com.zopim.android.sdk.chatlog.i */
class C1211i extends Adapter<ViewHolder> {

    /* renamed from: a */
    private static final String f858a = C1211i.class.getSimpleName();

    /* renamed from: b */
    private static final int f859b = C1179a.values().length;

    /* renamed from: c */
    private List<C1178aa> f860c = Collections.emptyList();

    /* renamed from: d */
    private Context f861d;
    /* access modifiers changed from: private */

    /* renamed from: e */
    public Chat f862e;

    /* renamed from: f */
    private final Object f863f = new Object();

    private C1211i() {
    }

    C1211i(Context context, List<C1178aa> list) {
        this.f860c = list;
        this.f861d = context;
    }

    /* renamed from: a */
    private void m700a(View view, boolean z) {
        if (view == null) {
            Log.w(f858a, "View must not be null. Skipping row item padding update.");
        } else if (z) {
            try {
                view.setPadding(view.getPaddingLeft(), (int) this.f861d.getResources().getDimension(C1122R.dimen.lead_message_padding_top), view.getPaddingRight(), view.getPaddingBottom());
            } catch (NotFoundException e) {
                Log.w(f858a, "Can not find padding dimension.Skipping.", e);
            }
        } else {
            view.setPadding(view.getPaddingLeft(), (int) this.f861d.getResources().getDimension(C1122R.dimen.chat_message_padding_top), view.getPaddingRight(), view.getPaddingBottom());
        }
    }

    /* renamed from: a */
    private boolean m701a(C1179a aVar, int i) {
        if (aVar != C1179a.m684a(getItemViewType(i - 1))) {
            return true;
        }
        String str = mo20759b(i).f797k;
        return str == null || !str.equals(mo20759b(i + -1).f797k);
    }

    /* renamed from: a */
    public void mo20756a(int i) {
        try {
            this.f860c.remove(i);
            notifyItemRemoved(i);
        } catch (UnsupportedOperationException e) {
            Log.w(f858a, "Can not remove an item from the adapter.", e);
        } catch (IndexOutOfBoundsException e2) {
            Log.w(f858a, "Can not remove item. Item does not exist.", e2);
        }
    }

    /* renamed from: a */
    public void mo20757a(Chat chat) {
        this.f862e = chat;
    }

    /* renamed from: a */
    public void mo20758a(C1178aa aaVar) {
        synchronized (this.f863f) {
            this.f860c.add(aaVar);
        }
    }

    /* renamed from: b */
    public C1178aa mo20759b(int i) {
        return (this.f860c == null || i < 0 || i >= this.f860c.size()) ? new C1178aa() : (C1178aa) this.f860c.get(i);
    }

    public int getItemCount() {
        return this.f860c.size();
    }

    public int getItemViewType(int i) {
        if (this.f860c == null || i < 0 || i >= this.f860c.size()) {
            return C1179a.UNKNOWN.mo20726a();
        }
        C1178aa aaVar = (C1178aa) this.f860c.get(i);
        return aaVar == null ? C1179a.UNKNOWN.mo20726a() : aaVar.f794h.mo20726a();
    }

    /* JADX WARNING: Removed duplicated region for block: B:30:0x00ab A[LOOP:1: B:28:0x00a6->B:30:0x00ab, LOOP_END] */
    /* Code decompiled incorrectly, please refer to instructions dump. */
    public void onBindViewHolder(android.support.v7.widget.RecyclerView.ViewHolder r9, int r10) {
        /*
            r8 = this;
            r4 = 0
            com.zopim.android.sdk.chatlog.aa r1 = r8.mo20759b(r10)
            int r0 = r8.getItemViewType(r10)
            com.zopim.android.sdk.chatlog.aa$a r0 = com.zopim.android.sdk.chatlog.C1178aa.C1179a.m684a(r0)
            int[] r2 = com.zopim.android.sdk.chatlog.C1215m.f867a
            int r0 = r0.ordinal()
            r0 = r2[r0]
            switch(r0) {
                case 1: goto L_0x0020;
                case 2: goto L_0x0020;
                case 3: goto L_0x003c;
                case 4: goto L_0x00ba;
                case 5: goto L_0x00d6;
                case 6: goto L_0x00e1;
                case 7: goto L_0x00ec;
                default: goto L_0x0018;
            }
        L_0x0018:
            java.lang.String r0 = f858a
            java.lang.String r1 = "Can not inflate unknown adapter item type. This may cause NullPointerException down the line."
            android.util.Log.w(r0, r1)
        L_0x001f:
            return
        L_0x0020:
            boolean r0 = r9 instanceof com.zopim.android.sdk.chatlog.VisitorMessageHolder
            if (r0 == 0) goto L_0x001f
            boolean r0 = r1 instanceof com.zopim.android.sdk.chatlog.C1180ab
            if (r0 == 0) goto L_0x001f
            r0 = r9
            com.zopim.android.sdk.chatlog.VisitorMessageHolder r0 = (com.zopim.android.sdk.chatlog.VisitorMessageHolder) r0
            com.zopim.android.sdk.chatlog.ab r1 = (com.zopim.android.sdk.chatlog.C1180ab) r1
            r0.mo20715a(r1)
            com.zopim.android.sdk.chatlog.aa$a r0 = com.zopim.android.sdk.chatlog.C1178aa.C1179a.VISITOR
            boolean r0 = r8.m701a(r0, r10)
            android.view.View r1 = r9.itemView
            r8.m700a(r1, r0)
            goto L_0x001f
        L_0x003c:
            boolean r0 = r9 instanceof com.zopim.android.sdk.chatlog.AgentMessageHolder
            if (r0 == 0) goto L_0x001f
            boolean r0 = r1 instanceof com.zopim.android.sdk.chatlog.C1177a
            if (r0 == 0) goto L_0x001f
            com.zopim.android.sdk.chatlog.AgentMessageHolder r9 = (com.zopim.android.sdk.chatlog.AgentMessageHolder) r9
            r0 = r1
            com.zopim.android.sdk.chatlog.a r0 = (com.zopim.android.sdk.chatlog.C1177a) r0
            com.zopim.android.sdk.chatlog.aa$a r2 = com.zopim.android.sdk.chatlog.C1178aa.C1179a.AGENT
            boolean r2 = r8.m701a(r2, r10)
            r9.mo20706a(r2)
            android.view.View r3 = r9.itemView
            r8.m700a(r3, r2)
            java.util.List<com.zopim.android.sdk.chatlog.aa> r2 = r8.f860c
            java.util.Iterator r5 = r2.iterator()
            r3 = r4
        L_0x005e:
            boolean r2 = r5.hasNext()
            if (r2 == 0) goto L_0x0084
            java.lang.Object r2 = r5.next()
            com.zopim.android.sdk.chatlog.aa r2 = (com.zopim.android.sdk.chatlog.C1178aa) r2
            com.zopim.android.sdk.chatlog.aa$a r6 = com.zopim.android.sdk.chatlog.C1178aa.C1179a.AGENT
            com.zopim.android.sdk.chatlog.aa$a r7 = r2.f794h
            if (r6 != r7) goto L_0x00fd
            java.lang.String r6 = r2.f797k
            java.lang.String r7 = r1.f797k
            boolean r6 = r6.equals(r7)
            if (r6 == 0) goto L_0x00fd
            java.lang.Long r2 = r2.f798l
            java.lang.Long r6 = r1.f798l
            if (r2 != r6) goto L_0x0084
            r3 = 1
            r2 = r3
        L_0x0082:
            r3 = r2
            goto L_0x005e
        L_0x0084:
            r9.mo20707b(r3)
            android.widget.LinearLayout r1 = r9.f748a
            r1.removeAllViews()
            java.lang.String[] r1 = r0.f792f
            if (r1 == 0) goto L_0x00b5
            java.lang.String[] r1 = r0.f792f
            int r1 = r1.length
            if (r1 <= 0) goto L_0x00b5
            java.lang.String r1 = f858a
            java.lang.String r2 = "Inflating agent questionnaire view"
            com.zopim.android.sdk.api.Logger.m577v(r1, r2)
            android.content.Context r1 = r8.f861d
            java.lang.String r2 = "layout_inflater"
            java.lang.Object r1 = r1.getSystemService(r2)
            android.view.LayoutInflater r1 = (android.view.LayoutInflater) r1
        L_0x00a6:
            java.lang.String[] r2 = r0.f792f
            int r2 = r2.length
            if (r4 >= r2) goto L_0x00b5
            int r2 = com.zopim.android.sdk.C1122R.C1126layout.questinnaire_option
            android.widget.LinearLayout r3 = r9.f748a
            r1.inflate(r2, r3)
            int r4 = r4 + 1
            goto L_0x00a6
        L_0x00b5:
            r9.mo20705a(r0)
            goto L_0x001f
        L_0x00ba:
            boolean r0 = r9 instanceof com.zopim.android.sdk.chatlog.C1208f
            if (r0 == 0) goto L_0x001f
            r0 = r9
            com.zopim.android.sdk.chatlog.f r0 = (com.zopim.android.sdk.chatlog.C1208f) r0
            com.zopim.android.sdk.chatlog.g r1 = (com.zopim.android.sdk.chatlog.C1209g) r1
            com.zopim.android.sdk.chatlog.aa$a r2 = com.zopim.android.sdk.chatlog.C1178aa.C1179a.AGENT
            boolean r2 = r8.m701a(r2, r10)
            r0.mo20753a(r2)
            r0.mo20752a(r1)
            android.view.View r0 = r9.itemView
            r8.m700a(r0, r2)
            goto L_0x001f
        L_0x00d6:
            boolean r0 = r9 instanceof com.zopim.android.sdk.chatlog.C1210h
            if (r0 == 0) goto L_0x001f
            com.zopim.android.sdk.chatlog.h r9 = (com.zopim.android.sdk.chatlog.C1210h) r9
            r9.mo20755a(r1)
            goto L_0x001f
        L_0x00e1:
            boolean r0 = r9 instanceof com.zopim.android.sdk.chatlog.C1216n
            if (r0 == 0) goto L_0x001f
            com.zopim.android.sdk.chatlog.n r9 = (com.zopim.android.sdk.chatlog.C1216n) r9
            r9.mo20764a(r1)
            goto L_0x001f
        L_0x00ec:
            boolean r0 = r9 instanceof com.zopim.android.sdk.chatlog.ChatRatingHolder
            if (r0 == 0) goto L_0x001f
            boolean r0 = r1 instanceof com.zopim.android.sdk.chatlog.C1222t
            if (r0 == 0) goto L_0x001f
            com.zopim.android.sdk.chatlog.ChatRatingHolder r9 = (com.zopim.android.sdk.chatlog.ChatRatingHolder) r9
            com.zopim.android.sdk.chatlog.t r1 = (com.zopim.android.sdk.chatlog.C1222t) r1
            r9.mo20709a(r1)
            goto L_0x001f
        L_0x00fd:
            r2 = r3
            goto L_0x0082
        */
        throw new UnsupportedOperationException("Method not decompiled: com.zopim.android.sdk.chatlog.C1211i.onBindViewHolder(android.support.v7.widget.RecyclerView$ViewHolder, int):void");
    }

    public ViewHolder onCreateViewHolder(ViewGroup viewGroup, int i) {
        C1179a a = C1179a.m684a(i);
        switch (C1215m.f867a[a.ordinal()]) {
            case 1:
            case 2:
                return new VisitorMessageHolder(LayoutInflater.from(viewGroup.getContext()).inflate(C1122R.C1126layout.row_visitor_message, viewGroup, false), new C1212j(this));
            case 3:
                return new AgentMessageHolder(LayoutInflater.from(viewGroup.getContext()).inflate(C1122R.C1126layout.row_agent_message, viewGroup, false), new C1213k(this));
            case 4:
                return new C1208f(LayoutInflater.from(viewGroup.getContext()).inflate(C1122R.C1126layout.row_agent_typing, viewGroup, false));
            case 5:
                return new C1210h(LayoutInflater.from(viewGroup.getContext()).inflate(C1122R.C1126layout.row_event_message, viewGroup, false));
            case 6:
                return new C1216n(LayoutInflater.from(viewGroup.getContext()).inflate(C1122R.C1126layout.row_member_event, viewGroup, false));
            case 7:
                return new ChatRatingHolder(LayoutInflater.from(viewGroup.getContext()).inflate(C1122R.C1126layout.row_chat_rating, viewGroup, false), new C1214l(this));
            default:
                Log.w(f858a, "Can not inflate " + a + " adapter item type. This may cause NullPointerException down the line.");
                return null;
        }
    }
}
