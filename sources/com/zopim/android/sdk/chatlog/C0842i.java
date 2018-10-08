package com.zopim.android.sdk.chatlog;

import android.content.Context;
import android.support.v7.widget.RecyclerView.Adapter;
import android.support.v7.widget.RecyclerView.ViewHolder;
import android.util.Log;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import com.zopim.android.sdk.C0785R;
import com.zopim.android.sdk.api.Chat;
import com.zopim.android.sdk.api.Logger;
import com.zopim.android.sdk.chatlog.aa.C0834a;
import java.util.Collections;
import java.util.List;

/* renamed from: com.zopim.android.sdk.chatlog.i */
class C0842i extends Adapter<ViewHolder> {
    /* renamed from: a */
    private static final String f814a = C0842i.class.getSimpleName();
    /* renamed from: b */
    private static final int f815b = C0834a.values().length;
    /* renamed from: c */
    private List<aa> f816c = Collections.emptyList();
    /* renamed from: d */
    private Context f817d;
    /* renamed from: e */
    private Chat f818e;
    /* renamed from: f */
    private final Object f819f = new Object();

    private C0842i() {
    }

    C0842i(Context context, List<aa> list) {
        this.f816c = list;
        this.f817d = context;
    }

    /* renamed from: a */
    private void m687a(View view, boolean z) {
        if (view == null) {
            Log.w(f814a, "View must not be null. Skipping row item padding update.");
        } else if (z) {
            try {
                view.setPadding(view.getPaddingLeft(), (int) this.f817d.getResources().getDimension(C0785R.dimen.lead_message_padding_top), view.getPaddingRight(), view.getPaddingBottom());
            } catch (Throwable e) {
                Log.w(f814a, "Can not find padding dimension.Skipping.", e);
            }
        } else {
            view.setPadding(view.getPaddingLeft(), (int) this.f817d.getResources().getDimension(C0785R.dimen.chat_message_padding_top), view.getPaddingRight(), view.getPaddingBottom());
        }
    }

    /* renamed from: a */
    private boolean m688a(C0834a c0834a, int i) {
        if (c0834a != C0834a.m671a(getItemViewType(i - 1))) {
            return true;
        }
        String str = m692b(i).f747k;
        return str == null || !str.equals(m692b(i - 1).f747k);
    }

    /* renamed from: a */
    public void m689a(int i) {
        try {
            this.f816c.remove(i);
            notifyItemRemoved(i);
        } catch (Throwable e) {
            Log.w(f814a, "Can not remove an item from the adapter.", e);
        } catch (Throwable e2) {
            Log.w(f814a, "Can not remove item. Item does not exist.", e2);
        }
    }

    /* renamed from: a */
    public void m690a(Chat chat) {
        this.f818e = chat;
    }

    /* renamed from: a */
    public void m691a(aa aaVar) {
        synchronized (this.f819f) {
            this.f816c.add(aaVar);
        }
    }

    /* renamed from: b */
    public aa m692b(int i) {
        return (this.f816c == null || i < 0 || i >= this.f816c.size()) ? new aa() : (aa) this.f816c.get(i);
    }

    public int getItemCount() {
        return this.f816c.size();
    }

    public int getItemViewType(int i) {
        if (this.f816c == null || i < 0 || i >= this.f816c.size()) {
            return C0834a.UNKNOWN.m672a();
        }
        aa aaVar = (aa) this.f816c.get(i);
        return aaVar == null ? C0834a.UNKNOWN.m672a() : aaVar.f744h.m672a();
    }

    public void onBindViewHolder(ViewHolder viewHolder, int i) {
        int i2 = 0;
        aa b = m692b(i);
        boolean a;
        switch (C0846m.f823a[C0834a.m671a(getItemViewType(i)).ordinal()]) {
            case 1:
            case 2:
                if ((viewHolder instanceof VisitorMessageHolder) && (b instanceof ab)) {
                    ((VisitorMessageHolder) viewHolder).m666a((ab) b);
                    m687a(viewHolder.itemView, m688a(C0834a.VISITOR, i));
                    return;
                }
                return;
            case 3:
                if ((viewHolder instanceof AgentMessageHolder) && (b instanceof C0833a)) {
                    LayoutInflater layoutInflater;
                    AgentMessageHolder agentMessageHolder = (AgentMessageHolder) viewHolder;
                    C0833a c0833a = (C0833a) b;
                    a = m688a(C0834a.AGENT, i);
                    agentMessageHolder.m655a(a);
                    m687a(agentMessageHolder.itemView, a);
                    boolean z = false;
                    for (aa aaVar : this.f816c) {
                        if (C0834a.AGENT != aaVar.f744h || !aaVar.f747k.equals(b.f747k)) {
                            a = z;
                        } else if (aaVar.f748l == b.f748l) {
                            a = true;
                        } else {
                            agentMessageHolder.m656b(z);
                            agentMessageHolder.f704a.removeAllViews();
                            if (c0833a.f754f != null && c0833a.f754f.length > 0) {
                                Logger.m564v(f814a, "Inflating agent questionnaire view");
                                layoutInflater = (LayoutInflater) this.f817d.getSystemService("layout_inflater");
                                while (i2 < c0833a.f754f.length) {
                                    layoutInflater.inflate(C0785R.layout.questinnaire_option, agentMessageHolder.f704a);
                                    i2++;
                                }
                            }
                            agentMessageHolder.m654a(c0833a);
                            return;
                        }
                        z = a;
                    }
                    agentMessageHolder.m656b(z);
                    agentMessageHolder.f704a.removeAllViews();
                    Logger.m564v(f814a, "Inflating agent questionnaire view");
                    layoutInflater = (LayoutInflater) this.f817d.getSystemService("layout_inflater");
                    while (i2 < c0833a.f754f.length) {
                        layoutInflater.inflate(C0785R.layout.questinnaire_option, agentMessageHolder.f704a);
                        i2++;
                    }
                    agentMessageHolder.m654a(c0833a);
                    return;
                }
                return;
            case 4:
                if (viewHolder instanceof C0839f) {
                    C0839f c0839f = (C0839f) viewHolder;
                    C0840g c0840g = (C0840g) b;
                    a = m688a(C0834a.AGENT, i);
                    c0839f.m682a(a);
                    c0839f.m681a(c0840g);
                    m687a(viewHolder.itemView, a);
                    return;
                }
                return;
            case 5:
                if (viewHolder instanceof C0841h) {
                    ((C0841h) viewHolder).m685a(b);
                    return;
                }
                return;
            case 6:
                if (viewHolder instanceof C0847n) {
                    ((C0847n) viewHolder).m693a(b);
                    return;
                }
                return;
            case 7:
                if ((viewHolder instanceof ChatRatingHolder) && (b instanceof C0853t)) {
                    ((ChatRatingHolder) viewHolder).m661a((C0853t) b);
                    return;
                }
                return;
            default:
                Log.w(f814a, "Can not inflate unknown adapter item type. This may cause NullPointerException down the line.");
                return;
        }
    }

    public ViewHolder onCreateViewHolder(ViewGroup viewGroup, int i) {
        C0834a a = C0834a.m671a(i);
        switch (C0846m.f823a[a.ordinal()]) {
            case 1:
            case 2:
                return new VisitorMessageHolder(LayoutInflater.from(viewGroup.getContext()).inflate(C0785R.layout.row_visitor_message, viewGroup, false), new C0843j(this));
            case 3:
                return new AgentMessageHolder(LayoutInflater.from(viewGroup.getContext()).inflate(C0785R.layout.row_agent_message, viewGroup, false), new C0844k(this));
            case 4:
                return new C0839f(LayoutInflater.from(viewGroup.getContext()).inflate(C0785R.layout.row_agent_typing, viewGroup, false));
            case 5:
                return new C0841h(LayoutInflater.from(viewGroup.getContext()).inflate(C0785R.layout.row_event_message, viewGroup, false));
            case 6:
                return new C0847n(LayoutInflater.from(viewGroup.getContext()).inflate(C0785R.layout.row_member_event, viewGroup, false));
            case 7:
                return new ChatRatingHolder(LayoutInflater.from(viewGroup.getContext()).inflate(C0785R.layout.row_chat_rating, viewGroup, false), new C0845l(this));
            default:
                Log.w(f814a, "Can not inflate " + a + " adapter item type. This may cause NullPointerException down the line.");
                return null;
        }
    }
}
