.class public Lcom/helpshift/support/fragments/DynamicFormFragment;
.super Lcom/helpshift/support/fragments/MainFragment;
.source "DynamicFormFragment.java"

# interfaces
.implements Landroid/view/View$OnClickListener;


# static fields
.field public static final FRAGMENT_TAG:Ljava/lang/String; = "HSDynamicFormFragment"


# instance fields
.field private flowList:Ljava/util/List;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "Ljava/util/List<",
            "Lcom/helpshift/support/flows/Flow;",
            ">;"
        }
    .end annotation
.end field

.field private flowListView:Landroidx/recyclerview/widget/RecyclerView;

.field private sendAnalyticsEvent:Z

.field private supportController:Lcom/helpshift/support/controllers/SupportController;

.field private title:Ljava/lang/String;


# direct methods
.method public constructor <init>()V
    .locals 1

    .line 26
    invoke-direct {p0}, Lcom/helpshift/support/fragments/MainFragment;-><init>()V

    const/4 v0, 0x1

    .line 32
    iput-boolean v0, p0, Lcom/helpshift/support/fragments/DynamicFormFragment;->sendAnalyticsEvent:Z

    return-void
.end method

.method public static newInstance(Landroid/os/Bundle;Ljava/util/List;Lcom/helpshift/support/controllers/SupportController;)Lcom/helpshift/support/fragments/DynamicFormFragment;
    .locals 1
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Landroid/os/Bundle;",
            "Ljava/util/List<",
            "Lcom/helpshift/support/flows/Flow;",
            ">;",
            "Lcom/helpshift/support/controllers/SupportController;",
            ")",
            "Lcom/helpshift/support/fragments/DynamicFormFragment;"
        }
    .end annotation

    .line 38
    new-instance v0, Lcom/helpshift/support/fragments/DynamicFormFragment;

    invoke-direct {v0}, Lcom/helpshift/support/fragments/DynamicFormFragment;-><init>()V

    .line 39
    invoke-virtual {v0, p0}, Lcom/helpshift/support/fragments/DynamicFormFragment;->setArguments(Landroid/os/Bundle;)V

    .line 40
    iput-object p1, v0, Lcom/helpshift/support/fragments/DynamicFormFragment;->flowList:Ljava/util/List;

    .line 41
    iput-object p2, v0, Lcom/helpshift/support/fragments/DynamicFormFragment;->supportController:Lcom/helpshift/support/controllers/SupportController;

    return-object v0
.end method

.method private performAction(Lcom/helpshift/support/flows/Flow;)V
    .locals 2

    .line 97
    instance-of v0, p1, Lcom/helpshift/support/flows/ConversationFlow;

    if-eqz v0, :cond_0

    .line 98
    move-object v0, p1

    check-cast v0, Lcom/helpshift/support/flows/ConversationFlow;

    iget-object v1, p0, Lcom/helpshift/support/fragments/DynamicFormFragment;->supportController:Lcom/helpshift/support/controllers/SupportController;

    invoke-virtual {v0, v1}, Lcom/helpshift/support/flows/ConversationFlow;->setSupportController(Lcom/helpshift/support/controllers/SupportController;)V

    goto :goto_0

    .line 100
    :cond_0
    instance-of v0, p1, Lcom/helpshift/support/flows/FAQSectionFlow;

    if-eqz v0, :cond_1

    .line 101
    move-object v0, p1

    check-cast v0, Lcom/helpshift/support/flows/FAQSectionFlow;

    iget-object v1, p0, Lcom/helpshift/support/fragments/DynamicFormFragment;->supportController:Lcom/helpshift/support/controllers/SupportController;

    invoke-virtual {v0, v1}, Lcom/helpshift/support/flows/FAQSectionFlow;->setSupportController(Lcom/helpshift/support/controllers/SupportController;)V

    goto :goto_0

    .line 103
    :cond_1
    instance-of v0, p1, Lcom/helpshift/support/flows/SingleFAQFlow;

    if-eqz v0, :cond_2

    .line 104
    move-object v0, p1

    check-cast v0, Lcom/helpshift/support/flows/SingleFAQFlow;

    iget-object v1, p0, Lcom/helpshift/support/fragments/DynamicFormFragment;->supportController:Lcom/helpshift/support/controllers/SupportController;

    invoke-virtual {v0, v1}, Lcom/helpshift/support/flows/SingleFAQFlow;->setSupportController(Lcom/helpshift/support/controllers/SupportController;)V

    goto :goto_0

    .line 106
    :cond_2
    instance-of v0, p1, Lcom/helpshift/support/flows/DynamicFormFlow;

    if-eqz v0, :cond_3

    .line 107
    move-object v0, p1

    check-cast v0, Lcom/helpshift/support/flows/DynamicFormFlow;

    iget-object v1, p0, Lcom/helpshift/support/fragments/DynamicFormFragment;->supportController:Lcom/helpshift/support/controllers/SupportController;

    invoke-virtual {v0, v1}, Lcom/helpshift/support/flows/DynamicFormFlow;->setSupportController(Lcom/helpshift/support/controllers/SupportController;)V

    goto :goto_0

    .line 109
    :cond_3
    instance-of v0, p1, Lcom/helpshift/support/flows/FAQsFlow;

    if-eqz v0, :cond_4

    .line 110
    move-object v0, p1

    check-cast v0, Lcom/helpshift/support/flows/FAQsFlow;

    iget-object v1, p0, Lcom/helpshift/support/fragments/DynamicFormFragment;->supportController:Lcom/helpshift/support/controllers/SupportController;

    invoke-virtual {v0, v1}, Lcom/helpshift/support/flows/FAQsFlow;->setSupportController(Lcom/helpshift/support/controllers/SupportController;)V

    .line 112
    :cond_4
    :goto_0
    invoke-interface {p1}, Lcom/helpshift/support/flows/Flow;->performAction()V

    return-void
.end method

.method private showFlows()V
    .locals 3

    .line 83
    iget-object v0, p0, Lcom/helpshift/support/fragments/DynamicFormFragment;->flowList:Ljava/util/List;

    if-eqz v0, :cond_0

    .line 84
    iget-object v0, p0, Lcom/helpshift/support/fragments/DynamicFormFragment;->flowListView:Landroidx/recyclerview/widget/RecyclerView;

    new-instance v1, Lcom/helpshift/support/adapters/FlowListAdapter;

    iget-object v2, p0, Lcom/helpshift/support/fragments/DynamicFormFragment;->flowList:Ljava/util/List;

    invoke-direct {v1, v2, p0}, Lcom/helpshift/support/adapters/FlowListAdapter;-><init>(Ljava/util/List;Landroid/view/View$OnClickListener;)V

    invoke-virtual {v0, v1}, Landroidx/recyclerview/widget/RecyclerView;->setAdapter(Landroidx/recyclerview/widget/RecyclerView$Adapter;)V

    :cond_0
    return-void
.end method


# virtual methods
.method public onClick(Landroid/view/View;)V
    .locals 1

    .line 90
    invoke-virtual {p1}, Landroid/view/View;->getTag()Ljava/lang/Object;

    move-result-object p1

    check-cast p1, Ljava/lang/Integer;

    invoke-virtual {p1}, Ljava/lang/Integer;->intValue()I

    move-result p1

    .line 91
    iget-object v0, p0, Lcom/helpshift/support/fragments/DynamicFormFragment;->flowList:Ljava/util/List;

    invoke-interface {v0, p1}, Ljava/util/List;->get(I)Ljava/lang/Object;

    move-result-object p1

    check-cast p1, Lcom/helpshift/support/flows/Flow;

    const/4 v0, 0x0

    .line 92
    iput-boolean v0, p0, Lcom/helpshift/support/fragments/DynamicFormFragment;->sendAnalyticsEvent:Z

    .line 93
    invoke-direct {p0, p1}, Lcom/helpshift/support/fragments/DynamicFormFragment;->performAction(Lcom/helpshift/support/flows/Flow;)V

    return-void
.end method

.method public onCreate(Landroid/os/Bundle;)V
    .locals 1
    .param p1    # Landroid/os/Bundle;
        .annotation build Landroidx/annotation/Nullable;
        .end annotation
    .end param

    .line 47
    invoke-super {p0, p1}, Lcom/helpshift/support/fragments/MainFragment;->onCreate(Landroid/os/Bundle;)V

    .line 49
    invoke-virtual {p0}, Lcom/helpshift/support/fragments/DynamicFormFragment;->getArguments()Landroid/os/Bundle;

    move-result-object p1

    if-eqz p1, :cond_0

    const-string v0, "flow_title"

    .line 51
    invoke-virtual {p1, v0}, Landroid/os/Bundle;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object p1

    iput-object p1, p0, Lcom/helpshift/support/fragments/DynamicFormFragment;->title:Ljava/lang/String;

    .line 52
    iget-object p1, p0, Lcom/helpshift/support/fragments/DynamicFormFragment;->title:Ljava/lang/String;

    invoke-static {p1}, Landroid/text/TextUtils;->isEmpty(Ljava/lang/CharSequence;)Z

    move-result p1

    if-eqz p1, :cond_0

    .line 53
    sget p1, Lcom/helpshift/R$string;->hs__help_header:I

    invoke-virtual {p0, p1}, Lcom/helpshift/support/fragments/DynamicFormFragment;->getString(I)Ljava/lang/String;

    move-result-object p1

    iput-object p1, p0, Lcom/helpshift/support/fragments/DynamicFormFragment;->title:Ljava/lang/String;

    :cond_0
    return-void
.end method

.method public onCreateView(Landroid/view/LayoutInflater;Landroid/view/ViewGroup;Landroid/os/Bundle;)Landroid/view/View;
    .locals 1
    .annotation build Landroidx/annotation/Nullable;
    .end annotation

    .line 61
    sget p3, Lcom/helpshift/R$layout;->hs__dynamic_form_fragment:I

    const/4 v0, 0x0

    invoke-virtual {p1, p3, p2, v0}, Landroid/view/LayoutInflater;->inflate(ILandroid/view/ViewGroup;Z)Landroid/view/View;

    move-result-object p1

    return-object p1
.end method

.method public onDestroyView()V
    .locals 1

    const/4 v0, 0x0

    .line 135
    iput-object v0, p0, Lcom/helpshift/support/fragments/DynamicFormFragment;->flowListView:Landroidx/recyclerview/widget/RecyclerView;

    .line 136
    invoke-super {p0}, Lcom/helpshift/support/fragments/MainFragment;->onDestroyView()V

    return-void
.end method

.method public onResume()V
    .locals 1

    .line 73
    invoke-super {p0}, Lcom/helpshift/support/fragments/MainFragment;->onResume()V

    .line 74
    iget-object v0, p0, Lcom/helpshift/support/fragments/DynamicFormFragment;->title:Ljava/lang/String;

    invoke-virtual {p0, v0}, Lcom/helpshift/support/fragments/DynamicFormFragment;->setToolbarTitle(Ljava/lang/String;)V

    .line 75
    invoke-direct {p0}, Lcom/helpshift/support/fragments/DynamicFormFragment;->showFlows()V

    return-void
.end method

.method public onStart()V
    .locals 2

    .line 117
    invoke-super {p0}, Lcom/helpshift/support/fragments/MainFragment;->onStart()V

    .line 118
    invoke-virtual {p0}, Lcom/helpshift/support/fragments/DynamicFormFragment;->isChangingConfigurations()Z

    move-result v0

    if-nez v0, :cond_0

    iget-boolean v0, p0, Lcom/helpshift/support/fragments/DynamicFormFragment;->sendAnalyticsEvent:Z

    if-eqz v0, :cond_0

    .line 119
    invoke-static {}, Lcom/helpshift/util/HelpshiftContext;->getCoreApi()Lcom/helpshift/CoreApi;

    move-result-object v0

    invoke-interface {v0}, Lcom/helpshift/CoreApi;->getAnalyticsEventDM()Lcom/helpshift/analytics/domainmodel/AnalyticsEventDM;

    move-result-object v0

    sget-object v1, Lcom/helpshift/analytics/AnalyticsEventType;->DYNAMIC_FORM_OPEN:Lcom/helpshift/analytics/AnalyticsEventType;

    invoke-virtual {v0, v1}, Lcom/helpshift/analytics/domainmodel/AnalyticsEventDM;->pushEvent(Lcom/helpshift/analytics/AnalyticsEventType;)V

    :cond_0
    const/4 v0, 0x1

    .line 121
    iput-boolean v0, p0, Lcom/helpshift/support/fragments/DynamicFormFragment;->sendAnalyticsEvent:Z

    return-void
.end method

.method public onStop()V
    .locals 2

    .line 126
    invoke-super {p0}, Lcom/helpshift/support/fragments/MainFragment;->onStop()V

    .line 127
    invoke-virtual {p0}, Lcom/helpshift/support/fragments/DynamicFormFragment;->isChangingConfigurations()Z

    move-result v0

    if-nez v0, :cond_0

    iget-boolean v0, p0, Lcom/helpshift/support/fragments/DynamicFormFragment;->sendAnalyticsEvent:Z

    if-eqz v0, :cond_0

    .line 128
    invoke-static {}, Lcom/helpshift/util/HelpshiftContext;->getCoreApi()Lcom/helpshift/CoreApi;

    move-result-object v0

    invoke-interface {v0}, Lcom/helpshift/CoreApi;->getAnalyticsEventDM()Lcom/helpshift/analytics/domainmodel/AnalyticsEventDM;

    move-result-object v0

    sget-object v1, Lcom/helpshift/analytics/AnalyticsEventType;->DYNAMIC_FORM_CLOSE:Lcom/helpshift/analytics/AnalyticsEventType;

    invoke-virtual {v0, v1}, Lcom/helpshift/analytics/domainmodel/AnalyticsEventDM;->pushEvent(Lcom/helpshift/analytics/AnalyticsEventType;)V

    :cond_0
    return-void
.end method

.method public onViewCreated(Landroid/view/View;Landroid/os/Bundle;)V
    .locals 1

    .line 66
    invoke-super {p0, p1, p2}, Lcom/helpshift/support/fragments/MainFragment;->onViewCreated(Landroid/view/View;Landroid/os/Bundle;)V

    .line 67
    sget p2, Lcom/helpshift/R$id;->flow_list:I

    invoke-virtual {p1, p2}, Landroid/view/View;->findViewById(I)Landroid/view/View;

    move-result-object p2

    check-cast p2, Landroidx/recyclerview/widget/RecyclerView;

    iput-object p2, p0, Lcom/helpshift/support/fragments/DynamicFormFragment;->flowListView:Landroidx/recyclerview/widget/RecyclerView;

    .line 68
    iget-object p2, p0, Lcom/helpshift/support/fragments/DynamicFormFragment;->flowListView:Landroidx/recyclerview/widget/RecyclerView;

    new-instance v0, Landroidx/recyclerview/widget/LinearLayoutManager;

    invoke-virtual {p1}, Landroid/view/View;->getContext()Landroid/content/Context;

    move-result-object p1

    invoke-direct {v0, p1}, Landroidx/recyclerview/widget/LinearLayoutManager;-><init>(Landroid/content/Context;)V

    invoke-virtual {p2, v0}, Landroidx/recyclerview/widget/RecyclerView;->setLayoutManager(Landroidx/recyclerview/widget/RecyclerView$LayoutManager;)V

    return-void
.end method

.method public setSupportController(Lcom/helpshift/support/controllers/SupportController;)V
    .locals 0

    .line 79
    iput-object p1, p0, Lcom/helpshift/support/fragments/DynamicFormFragment;->supportController:Lcom/helpshift/support/controllers/SupportController;

    return-void
.end method

.method public shouldRefreshMenu()Z
    .locals 1

    const/4 v0, 0x1

    return v0
.end method
