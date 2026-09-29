.class Lcom/helpshift/conversation/viewmodel/ListPickerVM$1;
.super Lcom/helpshift/common/domain/F;
.source "ListPickerVM.java"


# annotations
.annotation system Ldalvik/annotation/EnclosingMethod;
    value = Lcom/helpshift/conversation/viewmodel/ListPickerVM;->onListPickerSearchQueryChange(Ljava/lang/String;)V
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x0
    name = null
.end annotation


# instance fields
.field final synthetic this$0:Lcom/helpshift/conversation/viewmodel/ListPickerVM;

.field final synthetic val$searchQueryTokens:Ljava/util/List;


# direct methods
.method constructor <init>(Lcom/helpshift/conversation/viewmodel/ListPickerVM;Ljava/util/List;)V
    .locals 0

    .line 80
    iput-object p1, p0, Lcom/helpshift/conversation/viewmodel/ListPickerVM$1;->this$0:Lcom/helpshift/conversation/viewmodel/ListPickerVM;

    iput-object p2, p0, Lcom/helpshift/conversation/viewmodel/ListPickerVM$1;->val$searchQueryTokens:Ljava/util/List;

    invoke-direct {p0}, Lcom/helpshift/common/domain/F;-><init>()V

    return-void
.end method


# virtual methods
.method public f()V
    .locals 5

    .line 84
    new-instance v0, Ljava/util/ArrayList;

    const/4 v1, 0x3

    new-array v1, v1, [Lcom/helpshift/conversation/viewmodel/ListPickerVM$BaseOptionFilter;

    new-instance v2, Lcom/helpshift/conversation/viewmodel/ListPickerVM$SentencePrefixOptionFilter;

    iget-object v3, p0, Lcom/helpshift/conversation/viewmodel/ListPickerVM$1;->this$0:Lcom/helpshift/conversation/viewmodel/ListPickerVM;

    invoke-direct {v2, v3}, Lcom/helpshift/conversation/viewmodel/ListPickerVM$SentencePrefixOptionFilter;-><init>(Lcom/helpshift/conversation/viewmodel/ListPickerVM;)V

    const/4 v3, 0x0

    aput-object v2, v1, v3

    new-instance v2, Lcom/helpshift/conversation/viewmodel/ListPickerVM$WordPrefixOptionFilter;

    iget-object v3, p0, Lcom/helpshift/conversation/viewmodel/ListPickerVM$1;->this$0:Lcom/helpshift/conversation/viewmodel/ListPickerVM;

    invoke-direct {v2, v3}, Lcom/helpshift/conversation/viewmodel/ListPickerVM$WordPrefixOptionFilter;-><init>(Lcom/helpshift/conversation/viewmodel/ListPickerVM;)V

    const/4 v3, 0x1

    aput-object v2, v1, v3

    new-instance v2, Lcom/helpshift/conversation/viewmodel/ListPickerVM$WordSubStringFilter;

    iget-object v3, p0, Lcom/helpshift/conversation/viewmodel/ListPickerVM$1;->this$0:Lcom/helpshift/conversation/viewmodel/ListPickerVM;

    invoke-direct {v2, v3}, Lcom/helpshift/conversation/viewmodel/ListPickerVM$WordSubStringFilter;-><init>(Lcom/helpshift/conversation/viewmodel/ListPickerVM;)V

    const/4 v3, 0x2

    aput-object v2, v1, v3

    .line 85
    invoke-static {v1}, Ljava/util/Arrays;->asList([Ljava/lang/Object;)Ljava/util/List;

    move-result-object v1

    invoke-direct {v0, v1}, Ljava/util/ArrayList;-><init>(Ljava/util/Collection;)V

    .line 88
    new-instance v1, Lcom/helpshift/conversation/viewmodel/ListPickerVM$CompositeOptionFilter;

    iget-object v2, p0, Lcom/helpshift/conversation/viewmodel/ListPickerVM$1;->this$0:Lcom/helpshift/conversation/viewmodel/ListPickerVM;

    invoke-direct {v1, v2, v0}, Lcom/helpshift/conversation/viewmodel/ListPickerVM$CompositeOptionFilter;-><init>(Lcom/helpshift/conversation/viewmodel/ListPickerVM;Ljava/util/List;)V

    .line 89
    iget-object v0, p0, Lcom/helpshift/conversation/viewmodel/ListPickerVM$1;->this$0:Lcom/helpshift/conversation/viewmodel/ListPickerVM;

    invoke-static {v0}, Lcom/helpshift/conversation/viewmodel/ListPickerVM;->access$000(Lcom/helpshift/conversation/viewmodel/ListPickerVM;)Lcom/helpshift/conversation/activeconversation/message/OptionInputMessageDM;

    move-result-object v0

    iget-object v0, v0, Lcom/helpshift/conversation/activeconversation/message/OptionInputMessageDM;->input:Lcom/helpshift/conversation/activeconversation/message/input/OptionInput;

    iget-object v0, v0, Lcom/helpshift/conversation/activeconversation/message/input/OptionInput;->options:Ljava/util/List;

    iget-object v2, p0, Lcom/helpshift/conversation/viewmodel/ListPickerVM$1;->val$searchQueryTokens:Ljava/util/List;

    invoke-interface {v1, v0, v2}, Lcom/helpshift/conversation/viewmodel/ListPickerVM$OptionFilter;->filter(Ljava/util/List;Ljava/util/List;)Ljava/util/List;

    move-result-object v0

    .line 91
    invoke-interface {v0}, Ljava/util/List;->size()I

    move-result v1

    if-nez v1, :cond_0

    .line 92
    iget-object v0, p0, Lcom/helpshift/conversation/viewmodel/ListPickerVM$1;->this$0:Lcom/helpshift/conversation/viewmodel/ListPickerVM;

    invoke-static {v0}, Lcom/helpshift/conversation/viewmodel/ListPickerVM;->access$100(Lcom/helpshift/conversation/viewmodel/ListPickerVM;)V

    return-void

    .line 97
    :cond_0
    new-instance v1, Ljava/util/ArrayList;

    invoke-direct {v1}, Ljava/util/ArrayList;-><init>()V

    .line 98
    invoke-interface {v0}, Ljava/util/List;->iterator()Ljava/util/Iterator;

    move-result-object v0

    :goto_0
    invoke-interface {v0}, Ljava/util/Iterator;->hasNext()Z

    move-result v2

    if-eqz v2, :cond_1

    invoke-interface {v0}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v2

    check-cast v2, Lcom/helpshift/conversation/activeconversation/message/input/OptionInput$Option;

    .line 99
    iget-object v3, p0, Lcom/helpshift/conversation/viewmodel/ListPickerVM$1;->this$0:Lcom/helpshift/conversation/viewmodel/ListPickerVM;

    iget-object v4, p0, Lcom/helpshift/conversation/viewmodel/ListPickerVM$1;->val$searchQueryTokens:Ljava/util/List;

    invoke-static {v3, v2, v4}, Lcom/helpshift/conversation/viewmodel/ListPickerVM;->access$200(Lcom/helpshift/conversation/viewmodel/ListPickerVM;Lcom/helpshift/conversation/activeconversation/message/input/OptionInput$Option;Ljava/util/List;)Lcom/helpshift/conversation/viewmodel/OptionUIModel;

    move-result-object v2

    invoke-interface {v1, v2}, Ljava/util/List;->add(Ljava/lang/Object;)Z

    goto :goto_0

    .line 101
    :cond_1
    iget-object v0, p0, Lcom/helpshift/conversation/viewmodel/ListPickerVM$1;->this$0:Lcom/helpshift/conversation/viewmodel/ListPickerVM;

    invoke-static {v0, v1}, Lcom/helpshift/conversation/viewmodel/ListPickerVM;->access$300(Lcom/helpshift/conversation/viewmodel/ListPickerVM;Ljava/util/List;)V

    return-void
.end method
