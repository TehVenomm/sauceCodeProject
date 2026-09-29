.class public Lcom/helpshift/conversation/viewmodel/OptionUIModel;
.super Ljava/lang/Object;
.source "OptionUIModel.java"


# instance fields
.field public final option:Lcom/helpshift/conversation/activeconversation/message/input/OptionInput$Option;

.field public final titleHighlightInfo:Ljava/util/List;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "Ljava/util/List<",
            "Lcom/helpshift/conversation/viewmodel/HSRange;",
            ">;"
        }
    .end annotation
.end field


# direct methods
.method public constructor <init>(Lcom/helpshift/conversation/activeconversation/message/input/OptionInput$Option;Ljava/util/List;)V
    .locals 0
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Lcom/helpshift/conversation/activeconversation/message/input/OptionInput$Option;",
            "Ljava/util/List<",
            "Lcom/helpshift/conversation/viewmodel/HSRange;",
            ">;)V"
        }
    .end annotation

    .line 15
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    .line 16
    iput-object p1, p0, Lcom/helpshift/conversation/viewmodel/OptionUIModel;->option:Lcom/helpshift/conversation/activeconversation/message/input/OptionInput$Option;

    .line 17
    iput-object p2, p0, Lcom/helpshift/conversation/viewmodel/OptionUIModel;->titleHighlightInfo:Ljava/util/List;

    return-void
.end method
